using Bazooka.Core;
using DataAccess.Read;
using DataAccess.Write;
using Hangfire;
using Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Web.Controllers
{
    public class DeployController : ApiController
    {

        private ReadContext db = new ReadContext();

        [HttpGet]
        public ICollection<string> Search(int enviromentId, int applicationId)
        {
            var repos = db.DeploTasks
                          .Where(x => x.EnviromentId == enviromentId && x.ApplicationId == applicationId)
                          .Select(x => x.Repository)
                          .ToList();

            var package = db.DeploTasks
                          .Where(x => x.EnviromentId == enviromentId && x.ApplicationId == applicationId)
                          .Select(x => x.PackageName)
                          .First();

            return PackageSearcher.Search(repos, package);
        }

        [HttpGet]
        public void Deploy(int enviromentId,int applicationId, string version, ICollection<DeploymentTasksDto> tasks)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                var deploy = new Deployment()
                {
                    EnviromentId = enviromentId,
                    ApplicationId = applicationId,
                    Status = Status.Queud,
                    Version = version,
                    UserId = User.Identity.GetUserId()
                };

                if(tasks!= null && tasks.Count > 0)
                {
                    foreach(var task in tasks)
                    {
                        deploy.Tasks.Add(new DeploymentTask()
                        {
                            DeployTaskId = task.DeployTaskId,
                            DeployType = (int)task.DeployType
                        });
                    }
                }

                session.Save(deploy);
                session.Flush();

                BackgroundJob.Enqueue(() => DeployJob.Execute(deploy.Id));
            };
        }

        [HttpGet]
        public void Cancel(int deploymentId)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                var deploy = session.Load<Deployment>(deploymentId);

                if (deploy.Status == Status.Scheduled) {
                    deploy.Status = Status.Canceled;

                    session.Save(new DataAccess.Write.LogEntry()
                    {
                        DeploymentId = deploy.Id,
                        Error = false,
                        Text = "Deploy canceled",
                        TimeStamp = DateTime.UtcNow
                    });

                }

                session.Update(deploy);

                session.Flush();
            };
        }

        [HttpGet]
        public void Schedule(int enviromentId, int applicationId, string version, DateTime start, ICollection<DeploymentTasksDto> tasks)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                var deploy = new Deployment()
                {
                    EnviromentId = enviromentId,
                    Status = Status.Scheduled,
                    ApplicationId = applicationId,
                    Version = version,
                    UserId = User.Identity.GetUserId(),
                    StartDate = start.ToUniversalTime(),
                    Scheduled = true
                };

                if (tasks != null && tasks.Count > 0)
                {
                    foreach (var task in tasks)
                    {
                        deploy.Tasks.Add(new DeploymentTask()
                        {
                            DeployTaskId = task.DeployTaskId,
                            DeployType = (int)task.DeployType
                        });
                    }
                }

                session.Save(deploy);
                session.Flush();

                BackgroundJob.Schedule(() => DeployJob.Execute(deploy.Id),start.ToUniversalTime() - DateTime.UtcNow);
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}