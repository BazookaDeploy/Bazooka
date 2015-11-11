using DataAccess.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Web.Controllers
{
    public class DeploymentController : BaseController
    {
        public IReadContext ReadContext { get; set; }

        public ICollection<object> Filter(DateTime startDate)
        {
            return ReadContext.Query<DeploymentDto>()
                     .Where(x => x.StartDate == null || x.StartDate > startDate )
                     .OrderByDescending(x => x.StartDate ?? DateTime.UtcNow)
                     .Select(x => new { 
                        x.Configuration,
                        x.EndDate,
                        x.EnviromentId,
                        x.Id,
                        x.Name,
                        x.StartDate,
                        x.Status,
                        x.UserId,
                        x.UserName,
                        x.Version
                     }).ToList<object>();
        }


        public DeploymentDto Get(int id)
        {
            return ReadContext.Query<DeploymentDto>().Single(x => x.Id == id);
        }
    }
}