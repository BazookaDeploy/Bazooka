namespace Jobs
{
    using Bazooka.Core.Dto;
    using DataAccess.Read;
    using DataAccess.Write;
    using Newtonsoft.Json;
    using NHibernate;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

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
                session.Save(new LogEntry()
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
                        session.Save(new LogEntry()
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

                config = dc.Enviroments.Single(x => x.Id == envId).Configuration;
                var units = dc.DeploTasks.Where(x => x.EnviromentId == envId).ToList();

                foreach (var unit in units)
                {
                    try
                    {
                        var res = new List<string>();
                        ExecutionResult ret;
                        if (unit.CurrentlyDeployedVersion != null)
                        {
                            ret = Update(unit, version, config);
                        }
                        else
                        {
                            ret = Install(unit, version, config);
                        }
                        using (var session = Store.OpenSession())
                        {
                            foreach (var mess in ret.Log)
                            {
                                session.Save(new LogEntry()
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
                    }
                    catch (Exception e)
                    {
                        using (var session = Store.OpenSession())
                        {
                            var dep = session.Load<Deployment>(deploymentId);
                            dep.EndDate = DateTime.UtcNow;
                            dep.Status = Status.Failed;
                            session.Save(new LogEntry()
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
                        var deployUnit = session.Load<DeployTask>(unit.Id);
                        deployUnit.CurrentlyDeployedVersion = version;
                        session.Update(deployUnit);
                        session.Flush();
                    }
                }

            }

            using (var session = Store.OpenSession())
            {
                var dep = session.Load<Deployment>(deploymentId);
                dep.EndDate = DateTime.UtcNow;
                dep.Status = Status.Ended;
                session.Save(new LogEntry()
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





        private static ExecutionResult Install(DeployTaskDto unit, string version, string config)
        {
            var address = unit.Machine;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(unit.Machine);
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
                client.BaseAddress = new Uri(unit.Machine);
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
