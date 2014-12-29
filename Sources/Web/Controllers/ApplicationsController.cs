using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Web.Mapping;
using Web.Models;

namespace Web.Controllers
{
    public class ApplicationsController : ApiController
    {
        private ReadContext db =new ReadContext();

        // GET: api/Applications
        public ICollection<ApplicationDto> Get()
        {
            return db.Applications.ToList();
        }

        public void Post(string name) {
            using (var session=WebApiApplication.Store.OpenSession()){
                var Application = new Application() { Name = name };
                session.Save(Application);
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