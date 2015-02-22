using DataAccess.Read;
using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace Web.Controllers
{
    public class EnviromentsController : ApiController
    {
        private ReadContext db = new ReadContext();

        public ICollection<EnviromentDto> Get()
        {
            return db.Enviroments.ToList();
        }

        public ICollection<EnviromentDto> Get(int id)
        {
            return db.Enviroments.Where(x => x.ApplicationId == id).ToList();
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
