using Bazooka.Core;
using Bazooka.Core.Commands;
using DataAccess.Read;
using DataAccess.Write;
using Hangfire;
using Jobs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Web.Controllers
{
    public class DeployController : ApiController
    {

        private ReadContext db = new ReadContext();

        [HttpGet]
        public ICollection<string> Search(int enviromentId)
        {
            var repos = db.DeployUnits
                          .Where(x => x.EnviromentId == enviromentId)
                          .Select(x => x.Repository)
                          .ToList();

            var package = db.DeployUnits
                          .Where(x => x.EnviromentId == enviromentId)
                          .Select(x => x.PackageName)
                          .First();

            return PackageSearcher.Search(repos, package);
        }

        [HttpGet]
        public void Deploy(int enviromentId, string version)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                var deploy = new Deployment()
                {
                    EnviromentId = enviromentId,
                    Status = Status.Queud,
                    Version = version,
                    UserId = User.Identity.GetUserId()
                };

                session.Save(deploy);
                session.Flush();

                BackgroundJob.Enqueue(() => DeployJob.Execute(deploy.Id));
            };
        }

        [HttpGet]
        public void Schedule(int enviromentId, string version, DateTime start)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                var deploy = new Deployment()
                {
                    EnviromentId = enviromentId,
                    Status = Status.Scheduled,
                    Version = version,
                    UserId = User.Identity.GetUserId(),
                    StartDate = start
                };

                session.Save(deploy);
                session.Flush();

                BackgroundJob.Schedule(() => DeployJob.Execute(deploy.Id),start);
            };
        }

        [HttpGet]
        public void Begin(Guid deployKey, string version)
        {
            var env = db.Enviroments.Single(x => x.DeployKey == deployKey);

            using (var session = WebApiApplication.Store.OpenSession())
            {
                var deploy = new Deployment()
                {
                    EnviromentId = env.Id,
                    Status = Status.Queud,
                    Version = version,
                    UserId = User.Identity.GetUserId()
                };

                session.Save(deploy);
                session.Flush();

                BackgroundJob.Enqueue(() => DeployJob.Execute(deploy.Id));
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