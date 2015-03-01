using DataAccess.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Web.Controllers
{
    public class DeploymentController : ApiController
    {
        private ReadContext db = new ReadContext();

        public IQueryable<DeploymentDto> Get()
        {
            return db.Deployments;
        }


        public DeploymentDto Get(int id)
        {
            return db.Deployments.Single(x => x.Id == id);
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