namespace Web.Controllers
{
    using DataAccess.Read;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    public class UpdateController : ApiController
    {
        private ReadContext db = new ReadContext();

        [HttpGet]
        public ICollection<string> Agents()
        {
            return db.DeploTasks
                     .Select(x => x.Address.EndsWith("/") ? x.Address.Substring(0, x.Address.Length - 1) : x.Address)
                     .Distinct()
                     .ToList();
        }

        [HttpPost]
        public object Update(string agent)
        {
            var file = HttpContext.Current.Request.Files[0];


            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri(agent );
                return client.PostAsync("/api/update/update",new StreamContent( file.InputStream)).Result;
            }
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
