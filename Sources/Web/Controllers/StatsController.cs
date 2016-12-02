namespace Web.Controllers
{
    using DataAccess.Read;
    using System;
    using System.Linq;
    using System.Web.Http;

    public class StatsController : BaseController
    {
        public IReadContext db
        {
            get; set;
        }

        [HttpGet]
        public object Statistics(DateTime startDate)
        {
            var apps = db.Query<DeploymentDto>()
                            .Where(x => x.StartDate > startDate)
                            .Select(x => x.Name)
                            .OrderBy(x => x)
                            .Distinct()
                            .ToList();

            var deploys = db.Query<DeploymentDto>()
                            .Where(x => x.StartDate > startDate)
                            .GroupBy(x => new { x.Name, x.Configuration, x.EnviromentId} )
                            .Select(x => new { x.Key.Name, x.Key.Configuration, x.Key.EnviromentId, Count = x.Count()})
                            .GroupBy(x => x.Name)
                            .Select(x => new
                            {
                                app = x.Key,
                                envs = x.Select(z => new { z.Configuration, z.EnviromentId, z.Count}).OrderBy(y => y.EnviromentId),
                                total = x.Select(z => z.Count).Sum()
                            })
                            .OrderByDescending(x => x.total)
                            .ToList();

            var users = db.Query<DeploymentDto>()
                          .Where(x => x.StartDate > startDate)
                          .GroupBy(x => new { x.UserName })
                          .Select(x => new { x.Key.UserName, Count = x.Count() })
                          .OrderByDescending(x => x.Count)
                          .ToList();


            return new { Deploys = deploys, Users = users };
        }
    }
}
