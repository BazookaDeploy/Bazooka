using DataAccess.Read;
using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    public class DeployTasksController : ApiController
    {
        private ReadContext db = new ReadContext();

        // GET: api/Applications
         [HttpGet, Route("api/DeployTasks/Tasks")]
        public ICollection<DeployTaskDto> Tasks(int id)
        {
            return db.DeploTasks.Where(x => x.EnviromentId == id).ToList();
        }

        [HttpGet, Route("api/DeployTasks/DeployTask")]
         public DeployTaskDto DeployTask(int id)
        {
            return db.DeploTasks.Single(x => x.Id == id);
        }

        public void Put(DeployTask unit)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                foreach (var p in unit.AdditionalParameters)
                {
                    p.DeployTaskId = unit.Id;
                }
                session.SaveOrUpdate(unit);
                session.Flush();
            };
        }

        public void Post(DeployTask unit)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                var parameters = unit.AdditionalParameters;
                unit.AdditionalParameters = null;

                session.Save(unit);
                foreach (var p in parameters)
                {
                    p.DeployTaskId = unit.Id;
                }
                unit.AdditionalParameters = parameters;
                session.SaveOrUpdate(unit);

                session.Flush();
            };
        }

        [HttpGet, Route("api/DeployTasks/test")]
        public object Test(string url)
        {
            var client = new HttpClient();
            var res = client.GetAsync(url + "/api/health/ping").Result;
            return new
            {
                Success = res.IsSuccessStatusCode
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