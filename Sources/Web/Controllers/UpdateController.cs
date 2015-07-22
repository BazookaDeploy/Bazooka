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
            return db.DeployUnits
                     .Select(x => x.Machine.EndsWith("/") ? x.Machine.Substring(0, x.Machine.Length - 1) : x.Machine)
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
