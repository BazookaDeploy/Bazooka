namespace Controller
{
    using Bazooka.Core.Commands;
    using Bazooka.Core.Dto;
    using DataAccess.Read;
    using DataAccess.Write;
    using Newtonsoft.Json;
    using NServiceBus;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    public class DeployApplicationHandler : IHandleMessages<DeployApplication>
    {
        public void Handle(DeployApplication message)
        {
            int envId;
            string version;
            string config;
            using (var session = EndpointConfig.Store.OpenSession())
            {
                var dep = session.Load<Deployment>(message.DeploymentId);
                dep.StartDate = DateTime.UtcNow;
                dep.Status = Status.Running;
                version = dep.Version;
                dep.Log = "Deploy started \r\n";
                envId = dep.EnviromentId;
                session.Update(dep);
                session.Flush();
            }

            using (var dc = new ReadContext())
            {
                config = dc.Enviroments.Single(x => x.Id == envId).Configuration;
                var units = dc.DeployUnits.Where(x => x.EnviromentId == envId).ToList();

                foreach (var unit in units)
                {
                    try
                    {
                        var res = new List<string>();
                        if (unit.CurrentlyDeployedVersion != null)
                        {
                            var ret = Update(unit, version, config);
                            using (var session = EndpointConfig.Store.OpenSession())
                            {
                                var dep = session.Load<Deployment>(message.DeploymentId);
                                foreach (var mess in ret)
                                {
                                    dep.Log += mess;
                                }
                                session.Update(dep);
                                session.Flush();
                            }
                        }
                        else
                        {
                            var ret = Install(unit, version, config);
                            using (var session = EndpointConfig.Store.OpenSession())
                            {
                                var dep = session.Load<Deployment>(message.DeploymentId);
                                foreach (var mess in ret)
                                {
                                    dep.Log += mess;
                                }
                                session.Update(dep);
                                session.Flush();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        using (var session = EndpointConfig.Store.OpenSession())
                        {
                            var dep = session.Load<Deployment>(message.DeploymentId);
                            dep.EndDate = DateTime.UtcNow;
                            dep.Status = Status.Failed;
                            dep.Log += e.Message;
                            session.Update(dep);
                            session.Flush();
                        }

                        return;
                    }

                    using (var session = EndpointConfig.Store.OpenSession())
                    {
                        var deployUnit = session.Load<DeployUnit>(unit.Id);
                        deployUnit.CurrentlyDeployedVersion = version;
                        session.Update(deployUnit);
                        session.Flush();
                    }
                }

            }

            using (var session = EndpointConfig.Store.OpenSession())
            {
                var dep = session.Load<Deployment>(message.DeploymentId);
                dep.EndDate = DateTime.UtcNow;
                dep.Status = Status.Ended;
                dep.Log += "Deploy ended";
                session.Update(dep);
                session.Flush();
            }
        }





        private ICollection<string> Install(DeployUnitDto unit, string version, string config)
        {
            var address = unit.Machine;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(unit.Machine);

                var result2 = client.PostAsJsonAsync("/api/deploy/install", new
                {
                    Application = unit.PackageName,
                    Version = version,
                    Directory = unit.Directory,
                    Configuration = config,
                    Repository = unit.Repository,
                    AdditionalParameters = unit.Parameters
                });
                
                var result = result2.Result;
                var response = result.Content.ReadAsStringAsync().Result;

                var response2 = JsonConvert.DeserializeObject<ExecutionResult>(response);
                return new string[] { response2.Log, response2.Exception };
            }
        }


        private ICollection<string> Update(DeployUnitDto unit, string version, string config)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(unit.Machine);

                var result2 = client.PostAsJsonAsync("/api/deploy/update", new
                {
                    Application = unit.PackageName,
                    Version = version,
                    Directory = unit.Directory,
                    Configuration = config,
                    Repository = unit.Repository,
                    AdditionalParameters = unit.Parameters
                });

                var result = result2.Result;
                var response = result.Content.ReadAsStringAsync().Result;

                var response2 = JsonConvert.DeserializeObject<ExecutionResult>(response);
                return new string[] { response2.Log, response2.Exception };
            }
        }
    }
}
