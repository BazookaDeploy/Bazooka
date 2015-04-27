using DataAccess.Read;
using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Web.Controllers
{
    public class DeployUnitsController : ApiController
    {
        private ReadContext db = new ReadContext();

        // GET: api/Applications
         [HttpGet, Route("api/DeployUnits/Units")]
        public ICollection<DeployUnitDto> Units(int id)
        {
            return db.DeployUnits.Where(x => x.EnviromentId == id).ToList();
        }

        [HttpGet, Route("api/DeployUnits/DeployUnit")]
        public DeployUnitDto DeployUnit(int id)
        {
            return db.DeployUnits.Single(x => x.Id == id);
        }

        public void Put(DeployUnit unit)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                foreach (var p in unit.AdditionalParameters)
                {
                    p.DeployUnitId = unit.Id;
                }
                session.SaveOrUpdate(unit);
                session.Flush();
            };
        }

        public void Post(DeployUnit unit)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                var parameters = unit.AdditionalParameters;
                unit.AdditionalParameters = null;

                session.Save(unit);
                foreach (var p in parameters)
                {
                    p.DeployUnitId = unit.Id;
                }
                unit.AdditionalParameters = parameters;
                session.SaveOrUpdate(unit);

                session.Flush();
            };
        }

        [HttpGet, Route("api/DeployUnits/test")]
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