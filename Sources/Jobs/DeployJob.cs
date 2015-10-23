namespace Jobs
{
    using Bazooka.Core;
    using Bazooka.Core.Dto;
    using DataAccess.Read;
    using DataAccess.Write;
    using Newtonsoft.Json;
    using NHibernate;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Mail;

    /// <summary>
    ///     Job to deploy an application. It must be shared between web and controller so it has been 
    ///     extracted to a common project
    /// </summary>
    public class DeployJob
    {
        /// <summary>
        ///     Nhibernate store to use to save the data    
        /// </summary>
        public static ISessionFactory Store { get; set; }

        /// <summary>
        ///     Execute the job deployn the specified deployment
        /// </summary>
        /// <param name="deploymentId">Deployment identifier</param>
        public static void Execute(int deploymentId)
        {
            int envId;
            string version;
            string config;
            using (var session = Store.OpenSession())
            {
                var dep = session.Load<Deployment>(deploymentId);

                if (dep.Status == Status.Canceled)
                {
                    return;
                }

                dep.StartDate = DateTime.UtcNow;
                dep.Status = Status.Running;
                version = dep.Version;
                session.Save(new DataAccess.Write.LogEntry()
                {
                    DeploymentId = dep.Id,
                    Error = false,
                    Text = "Deploy started",
                    TimeStamp = DateTime.UtcNow
                });
                envId = dep.EnviromentId;
                session.Update(dep);
                session.Flush();
            }

            ICollection<TaskDto> tasks;
            using (var dc = new ReadContext())
            {
                var other = dc.Deployments.Where(x => x.EnviromentId == envId && x.Id != deploymentId && x.Status == Status.Running);

                if (other.Count() > 0)
                {
                    using (var session = Store.OpenSession())
                    {
                        var dep = session.Load<Deployment>(deploymentId);
                        dep.EndDate = DateTime.UtcNow;
                        dep.Status = Status.Failed;
                        session.Save(new DataAccess.Write.LogEntry()
                        {
                            DeploymentId = dep.Id,
                            Error = true,
                            Text = "There is another deployment currently running for the same application in the same enviroment",
                            TimeStamp = DateTime.UtcNow
                        });
                        session.Update(dep);
                        session.Flush();
                        return;
                    }

                }

                config = dc.Enviroments.Single(x => x.Id == envId).Name;
                tasks = dc.Tasks.Where(x => x.EnviromentId == envId).ToList();
            }


            try
            {
                foreach (var task in tasks)
                {
                    switch (task.Type)
                    {
                        case TaskType.Deploy:
                            DeployJob.DeployTask(task.Id, deploymentId, version, config);
                            break;
                        case TaskType.LocalScript:
                            DeployJob.LocalScriptTask(task.Id, deploymentId, version, config);
                            break;
                        case TaskType.Mail:
                            DeployJob.MailTask(task.Id, deploymentId, version, config);
                            break;
                        case TaskType.RemoteScript:
                            DeployJob.RemoteScriptTask(task.Id, deploymentId, version, config);
                            break;
                        case TaskType.Database:
                            DeployJob.DatabaseTask(task.Id, deploymentId, version, config);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                using (var session = Store.OpenSession())
                {
                    var dep = session.Load<Deployment>(deploymentId);
                    dep.EndDate = DateTime.UtcNow;
                    dep.Status = Status.Failed;
                    session.Save(new DataAccess.Write.LogEntry()
                    {
                        DeploymentId = dep.Id,
                        Error = true,
                        Text = e.Message,
                        TimeStamp = DateTime.UtcNow
                    });
                    session.Update(dep);
                    session.Flush();
                }
                return;
            }


            using (var session = Store.OpenSession())
            {
                var dep = session.Load<Deployment>(deploymentId);
                dep.EndDate = DateTime.UtcNow;
                dep.Status = Status.Ended;
                session.Save(new DataAccess.Write.LogEntry()
                {
                    DeploymentId = dep.Id,
                    Error = false,
                    Text = "Deploy ended",
                    TimeStamp = DateTime.UtcNow
                });
                session.Update(dep);
                session.Flush();
            }
        }

        private static void DatabaseTask(int id, int deploymentId, string version, string config)
        {
            using (var dc = new ReadContext())
            {
                var unit = dc.DatabaseTasks.Single(x => x.Id == id);

                var res = new List<string>();
                ExecutionResult ret;

                using (var session = Store.OpenSession())
                {
                    session.Save(new DataAccess.Write.LogEntry()
                    {
                        DeploymentId = deploymentId,
                        Error = false,
                        Text = "Deploying database" + unit.Name,
                        TimeStamp = DateTime.UtcNow
                    });

                    session.Flush();
                }

                var address = unit.AgentName;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(unit.AgentName);
                    client.Timeout = TimeSpan.FromSeconds(300);

                    var result2 = client.PostAsJsonAsync("/api/deploy/databaseDeploy", new DatabaseDeployDto()
                    {
                        ConnectionString = unit.ConnectionString,
                        DataBase = unit.DatabaseName,
                        PackageName = unit.Package,
                        Repository = unit.Repository,
                        Version = version
                    });

                    var result = result2.Result;
                    var response = result.Content.ReadAsStringAsync().Result;

                    ret = JsonConvert.DeserializeObject<ExecutionResult>(response);

                    using (var session = Store.OpenSession())
                    {

                        session.Save(new DataAccess.Write.LogEntry()
                        {
                            DeploymentId = deploymentId,
                            Error = false,
                            Text = String.Join("\r\n", ret.Log.Select(x => x.Text)),
                            TimeStamp = DateTime.UtcNow
                        });

                        session.Flush();
                    }

                }

                using (var session = Store.OpenSession())
                {
                    foreach (var mess in ret.Log)
                    {
                        session.Save(new DataAccess.Write.LogEntry()
                        {
                            DeploymentId = deploymentId,
                            Error = mess.Error,
                            Text = mess.Text,
                            TimeStamp = mess.TimeStamp
                        });
                    }
                    session.Flush();
                }
            }
        }

        private static void RemoteScriptTask(int id, int deploymentId, string version, string config)
        {
            using (var dc = new ReadContext())
            {
                var unit = dc.RemoteScriptTasks.Single(x => x.Id == id);

                var res = new List<string>();
                ExecutionResult ret;

                using (var session = Store.OpenSession())
                {
                    session.Save(new DataAccess.Write.LogEntry()
                    {
                        DeploymentId = deploymentId,
                        Error = false,
                        Text = "Executing remote script " + unit.Name,
                        TimeStamp = DateTime.UtcNow
                    });

                    session.Flush();
                }

                var address = unit.Address;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(unit.Address);
                    client.Timeout = TimeSpan.FromSeconds(300);

                    var result2 = client.PostAsJsonAsync("/api/deploy/executeScript", new RemoteScriptDto()
                    {
                        Folder = unit.Folder,
                        Script = unit.Script
                    });

                    var result = result2.Result;
                    var response = result.Content.ReadAsStringAsync().Result;

                    ret = JsonConvert.DeserializeObject<ExecutionResult>(response);

                    using (var session = Store.OpenSession())
                    {

                        session.Save(new DataAccess.Write.LogEntry()
                        {
                            DeploymentId = deploymentId,
                            Error = false,
                            Text = String.Join("\r\n", ret.Log.Select(x => x.Text)),
                            TimeStamp = DateTime.UtcNow
                        });

                        session.Flush();
                    }

                }

                using (var session = Store.OpenSession())
                {
                    foreach (var mess in ret.Log)
                    {
                        session.Save(new DataAccess.Write.LogEntry()
                        {
                            DeploymentId = deploymentId,
                            Error = mess.Error,
                            Text = mess.Text,
                            TimeStamp = mess.TimeStamp
                        });
                    }
                    session.Flush();
                }
            }
        }

        private static void LocalScriptTask(int id, int deploymentId, string version, string config)
        {
            using (var dc = new ReadContext())
            {
                var unit = dc.LocalScriptTasks.Single(x => x.Id == id);
                var cwd = Environment.CurrentDirectory;
                var logger = new StringLogger();
                using (var session = Store.OpenSession())
                {

                    session.Save(new DataAccess.Write.LogEntry()
                    {
                        DeploymentId = deploymentId,
                        Error = false,
                        Text = "Executing local script " + unit.Name,
                        TimeStamp = DateTime.UtcNow
                    });

                    session.Flush();
                }

                PowershellHelpers.ExecuteScript(cwd, unit.Script, logger, new Dictionary<string, string>());

                using (var session = Store.OpenSession())
                {

                    session.Save(new DataAccess.Write.LogEntry()
                    {
                        DeploymentId = deploymentId,
                        Error = false,
                        Text = String.Join("\r\n", logger.Logs.Select(x => x.Text)),
                        TimeStamp = DateTime.UtcNow
                    });

                    session.Flush();
                }
            }
        }

        private static void MailTask(int id, int deploymentId, string version, string config)
        {
            using (var dc = new ReadContext())
            {
                var unit = dc.MailTasks.Single(x => x.Id == id);

                MailMessage mail = new MailMessage(unit.Sender, unit.Recipients);
                SmtpClient client = new SmtpClient();
                mail.Subject = "BAZOOKA: deployed version " + version + " in " + config;
                mail.Body = unit.Text.Replace("[VERSION]", version)
                                     .Replace("[CONFIG]", config);

                client.Send(mail);

                using (var session = Store.OpenSession())
                {

                    session.Save(new DataAccess.Write.LogEntry()
                    {
                        DeploymentId = deploymentId,
                        Error = false,
                        Text = "Sent mail to " + unit.Recipients,
                        TimeStamp = DateTime.UtcNow
                    });

                    session.Flush();
                }
            }
        }

        private static void DeployTask(int id, int deploymentId, string version, string config)
        {
            using (var dc = new ReadContext())
            {
                var unit = dc.DeploTasks.Single(x => x.Id == id);

                var res = new List<string>();
                ExecutionResult ret;
                ret = unit.CurrentlyDeployedVersion != null ? Update(unit, version, config) : Install(unit, version, config);

                using (var session = Store.OpenSession())
                {
                    foreach (var mess in ret.Log)
                    {
                        session.Save(new DataAccess.Write.LogEntry()
                        {
                            DeploymentId = deploymentId,
                            Error = mess.Error,
                            Text = mess.Text,
                            TimeStamp = mess.TimeStamp
                        });
                    }
                    session.Flush();
                }

                if (!ret.Success)
                {
                    throw new Exception("Deploy failed");
                }

                using (var session = Store.OpenSession())
                {
                    var deployUnit = session.Load<DeployTask>(unit.Id);
                    deployUnit.CurrentlyDeployedVersion = version;
                    session.Update(deployUnit);
                    session.Flush();
                }
            }
        }

        private static ExecutionResult Install(DeployTaskDto unit, string version, string config)
        {
            var address = unit.Address;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(unit.Address);
                client.Timeout = TimeSpan.FromSeconds(300);

                var result2 = client.PostAsJsonAsync("/api/deploy/install", new
                InstallDto
                {
                    Application = unit.PackageName,
                    Version = version,
                    Directory = unit.Directory,
                    Configuration = unit.Configuration ?? config,
                    Repository = unit.Repository,
                    AdditionalParameters = unit.Parameters.ToDictionary(x => x.Name, x => x.Value),
                    ConfigurationFile = unit.ConfigurationFile,
                    ConfigurationTransform = unit.ConfigurationTransform,
                    InstallScript = unit.InstallScript,
                    UninstallScript = unit.UninstallScript
                });

                var result = result2.Result;
                var response = result.Content.ReadAsStringAsync().Result;

                var response2 = JsonConvert.DeserializeObject<ExecutionResult>(response);
                return response2;
            }
        }

        private static ExecutionResult Update(DeployTaskDto unit, string version, string config)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(unit.Address);
                client.Timeout = TimeSpan.FromSeconds(300);

                var result2 = client.PostAsJsonAsync("/api/deploy/update", new
                InstallDto
                {
                    Application = unit.PackageName,
                    Version = version,
                    Directory = unit.Directory,
                    Configuration = unit.Configuration ?? config,
                    Repository = unit.Repository,
                    AdditionalParameters = unit.Parameters.ToDictionary(x => x.Name, x => x.Value),
                    ConfigurationFile = unit.ConfigurationFile,
                    ConfigurationTransform = unit.ConfigurationTransform,
                    InstallScript = unit.InstallScript,
                    UninstallScript = unit.UninstallScript
                });

                var result = result2.Result;
                var response = result.Content.ReadAsStringAsync().Result;

                var response2 = JsonConvert.DeserializeObject<ExecutionResult>(response);
                return response2;
            }
        }
    }
}
