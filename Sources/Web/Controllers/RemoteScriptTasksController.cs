namespace Web.Controllers
{
    using DataAccess.Read;
    using DataAccess.Write;
    using System.Linq;
    using System.Web.Http;

    public class RemoteScriptTasksController : ApiController
    {
        private ReadContext db = new ReadContext();

        public RemoteScriptTaskDto Get(int id)
        {
            return db.RemoteScriptTasks.Single(x => x.Id == id);
        }

        public void Put(RemoteScriptTask unit)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                session.SaveOrUpdate(unit);
                session.Flush();
            };
        }

        public void Post(RemoteScriptTask unit)
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