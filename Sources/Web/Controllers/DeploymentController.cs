using DataAccess.Read;
using System;
using System.Linq;
using System.Web.Http;

namespace Web.Controllers
{
    public class DeploymentController : ApiController
    {
        private ReadContext db = new ReadContext();

        [Queryable]
        public IQueryable<object> Get()
        {
            return db.Deployments
                     .OrderByDescending(x => x.StartDate ?? DateTime.UtcNow)
                     .Select(x => new { 
                        x.Configuration,
                        x.EndDate,
                        x.EnviromentId,
                        x.Id,
                        x.Name,
                        x.StartDate,
                        x.Status,
                        x.UserId,
                        x.UserName,
                        x.Version,                        
                     });
        }


        public DeploymentDto Get(int id)
        {
            var dep =  db.Deployments.AsNoTracking().Include("Tasks").Single(x => x.Id == id);
            //dep.Logs = dep.Logs.OrderBy(x => x.TimeStamp).ToList();
            return new DeploymentDto() {
                ApplicationId = dep.ApplicationId,
                Configuration = dep.Configuration,
                EndDate = dep.EndDate,
                EnviromentId = dep.EnviromentId,
                Id = dep.Id,
                Logs = dep.Logs.OrderBy(x => x.TimeStamp).ToList(),
                Name = dep.Name,
                StartDate = dep.StartDate,
                Status = dep.Status,
                Tasks = dep.Tasks.ToList(),
                UserId = dep.UserId,
                UserName = dep.UserName,
                Version = dep.Version
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