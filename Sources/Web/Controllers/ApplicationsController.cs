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
        public IQueryable<Application> Get()
        {
            return db.Query<Application>();
        }

        public void Post(string name) {
            var Application = new Application() { Name = name };
            db.Save(Application);
            db.Flush();
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