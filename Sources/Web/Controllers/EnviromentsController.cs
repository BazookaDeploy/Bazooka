namespace Web.Controllers
{
    using DataAccess.Read;
    using DataAccess.Write;
    using Microsoft.AspNet.Identity;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    public class EnviromentsController : ApiController
    {
        private ReadContext db = new ReadContext();

        public ICollection<EnviromentDto> Get()
        {
            return db.Enviroments.ToList();
        }

        public void Post(Enviroment env)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                session.Save(env);
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
