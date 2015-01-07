using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Web.Mapping;
using Web.Models;

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
                session.Save(unit);
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