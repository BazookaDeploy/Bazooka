namespace Web.Controllers
{
    using DataAccess.Read;
    using DataAccess.Write;
    using System.Linq;
    using System.Web.Http;

    public class LocalScriptTasksController : ApiController
    {
        private ReadContext db = new ReadContext();

        public LocalScriptTaskDto Get(int id)
        {
            return db.LocalScriptTasks.Single(x => x.Id == id);
        }

        public void Put(LocalScriptTask unit)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                session.SaveOrUpdate(unit);
                session.Flush();
            };
        }

        public void Post(LocalScriptTask unit)
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

