namespace Web.Controllers
{
    using DataAccess.Read;
    using DataAccess.Write;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Commands;

    public class EnviromentsController : BaseController
    {
        private ReadContext db = new ReadContext();

        public ICollection<EnviromentDto> Get()
        {
            return db.Enviroments.ToList();
        }

        //public void Post(Enviroment env)
        //{
        //    using (var session = WebApiApplication.Store.OpenSession())
        //    {
        //        session.Save(env);
        //        session.Flush();
        //    };
        //}

        public ExecutionResult Create(CreateEnviroment command)
        {
            return Execute(command);
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
