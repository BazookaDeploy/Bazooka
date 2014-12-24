using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    public class ApplicationsController : ApiController
    {
        private ISession db = WebApiApplication.Store.OpenSession();

        // GET: api/Applications
        public IQueryable<Application> GetApplications()
        {
            return db.Query<Application>();
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