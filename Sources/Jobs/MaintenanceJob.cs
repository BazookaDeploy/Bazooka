using Bazooka.Core.Dto;
using DataAccess.Read;
using DataAccess.Write;
using Newtonsoft.Json;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Jobs
{
    public class MaintenanceJob
    {
        /// <summary>
        ///     Nhibernate store to use to save the data    
        /// </summary>
        public static ISessionFactory Store { get; set; }

        /// <summary>
        ///     Execute the job deployn the specified deployment
        /// </summary>
        /// <param name="deploymentId">Deployment identifier</param>
        public static void Execute(int maintenanceTaskId, Dictionary<string,string> parameters)
        {
            int taskId;
            int agentId;
            string version;
            string config;

            using (var session = Store.OpenSession())
            {
                var dep = session.Load<MaintenanceTask>(maintenanceTaskId);

                if (dep.Status == Status.Canceled)
                {
                    return;
                }

                dep.StartDate = DateTime.UtcNow;
                dep.Status = Status.Running;
                taskId = dep.TemplatedTaskId;
                agentId = dep.AgentId;
                session.Save(new DataAccess.Write.MaintenanceLogEntry()
                {
                    MaintenanceTaskId = dep.Id,
                    Text = "Deploy started",
                });

                session.Update(dep);
                session.Flush();
            }


            try
            {

               MaintenanceJob.TemplatedTask(taskId, maintenanceTaskId,agentId,  parameters);
            }
            catch (Exception e)
            {
                using (var session = Store.OpenSession())
                {
                    var dep = session.Load<MaintenanceTask>(maintenanceTaskId);
                    dep.Status = Status.Failed;
                    session.Save(new DataAccess.Write.MaintenanceLogEntry()
                    {
                        MaintenanceTaskId = dep.Id,
                        Text = e.Message,
                    });
                    session.Update(dep);
                    session.Flush();
                }
                return;
            }


            using (var session = Store.OpenSession())
            {
                var dep = session.Load<MaintenanceTask>(maintenanceTaskId);
                dep.Status = Status.Ended;
                session.Save(new DataAccess.Write.MaintenanceLogEntry()
                {
                    MaintenanceTaskId = dep.Id,
                    Text = "Deploy ended",
                });
                session.Update(dep);
                session.Flush();
            }
        }

        private static void TemplatedTask(int id, int maintenanceTaskId, int agentId, Dictionary<string,string> parameters)
        {
            using (var dc = new ReadContext())
            {
                var unit = dc.Tasktemplates.Single(x => x.Id == id);
                var version = dc.TaskTemplatesVersion.Where(x => x.TaskTemplateId == id).Last();
                var agent = dc.Agents.Single(x => x.Id == id);

                var res = new List<string>();
                ExecutionResult ret;

                using (var session = Store.OpenSession())
                {
                    session.Save(new DataAccess.Write.MaintenanceLogEntry()
                    {
                        MaintenanceTaskId = maintenanceTaskId,
                        Text = "Executing templated task " + unit.Name,
                    });

                    session.Flush();
                }

                var address = agent.Address;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    client.Timeout = TimeSpan.FromSeconds(300);

                    var result2 = client.PostAsJsonAsync("/api/deploy/ExecuteTemplatedTask", new Bazooka.Core.Dto.TemplatedTaskDto()
                    {
                        Script = version.Script,
                        Parameters = parameters.Select(x => new ParameterDto()
                        {
                            Name = x.Key,
                            Value = x.Value
                        }).ToList()
                    });

                    var result = result2.Result;
                    var response = result.Content.ReadAsStringAsync().Result;

                    ret = JsonConvert.DeserializeObject<ExecutionResult>(response);




                    using (var session = Store.OpenSession())
                    {
                        ret.Log.ToList().ForEach(x =>
                        {
                            session.Save(new DataAccess.Write.MaintenanceLogEntry()
                            {
                                MaintenanceTaskId = maintenanceTaskId,
                                Text = x.Text,
                            });
                        });

                        session.Save(new DataAccess.Write.MaintenanceLogEntry()
                        {
                            MaintenanceTaskId = maintenanceTaskId,
                            Text = "Templated task " + unit.Name + " executed",
                        });

                        session.Flush();
                    }


                    if (!ret.Success)
                    {
                        throw new Exception("Deploy failed: " + ret.Exception);
                    }

                }
            }
        }
    }
}
