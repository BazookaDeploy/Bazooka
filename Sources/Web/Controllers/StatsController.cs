namespace Web.Controllers
{
    using DataAccess.Read;
    using System;
    using System.Linq;
    using System.Web.Http;

    public class StatsController : ApiController
    {
        private ReadContext db = new ReadContext();

        [HttpGet]
        public object Statistics(DateTime startDate)
        {
               var apps = db.Deployments
                            .Where(x => x.StartDate > startDate)
                            .Select(x => x.Name)
                            .OrderBy(x => x)
                            .Distinct()
                            .ToList();

            var deploys = db.Deployments
                            .Where(x => x.StartDate > startDate)
                            .GroupBy(x => x.Configuration)
                            .Select(x => new
                            {
                                app = x.Key,
                                envs = apps.Select(z => new {
                                    env= z,
                                    count = x.Count(y => y.Name == z)
                                }).OrderBy(z => z.env).ToList()
                            })
                            .OrderBy(x => x.app)
                            .ToList();

            var users = db.Deployments
                          .Where(x => x.StartDate > startDate)
                          .GroupBy(x => new { x.UserName })
                          .Select(x => new { x.Key.UserName, Count = x.Count() })
                          .OrderByDescending(x => x.Count)
                          .ToList();

            return new { Deploys = deploys, Users = users };
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
