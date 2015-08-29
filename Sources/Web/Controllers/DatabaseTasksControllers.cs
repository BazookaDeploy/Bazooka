using DataAccess.Read;
using DataAccess.Write;
using System.Linq;
using System.Web.Http;

namespace Web.Controllers
{
    public class DatabaseTasksControllers : ApiController
    {
        private ReadContext db = new ReadContext();

        public DatabaseTaskDto Get(int id)
        {
            return db.DatabaseTasks.Single(x => x.Id == id);
        }

        public void Put(DatabaseTask unit)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                session.SaveOrUpdate(unit);
                session.Flush();
            };
        }

        public void Post(DatabaseTask unit)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
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
