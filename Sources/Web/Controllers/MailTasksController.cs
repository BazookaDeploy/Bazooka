namespace Web.Controllers
{
    using DataAccess.Read;
    using DataAccess.Write;
    using System.Linq;
    using System.Web.Http;

    public class MailTasksController : ApiController
    {
        private ReadContext db = new ReadContext();

        public MailTaskDto Get(int id)
        {
            return db.MailTasks.Single(x => x.Id == id);
        }

        public void Put(MailTask unit)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                session.SaveOrUpdate(unit);
                session.Flush();
            };
        }

        public void Post(MailTask unit)
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
