using DataAccess.Read;
using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Web.Controllers
{
    public class DeployUnitsController : ApiController
    {
        private ReadContext db = new ReadContext();

        // GET: api/Applications
        public ICollection<DeployUnitDto> Get()
        {
            return db.DeployUnits.ToList();
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