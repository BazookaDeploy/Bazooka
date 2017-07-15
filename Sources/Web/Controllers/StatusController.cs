using DataAccess.Read;
using System.Collections;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Web.Controllers
{
    public class StatusController : BaseController
    {
        public IReadContext db { get; set; }


        public object Get()
        {
            var id = User.Identity.GetUserId();

            var enviroments = db.Query<EnviromentDto>().OrderBy(x => x.Id).ToList();
            var deployable = db.Query<DeployersDto>().Where(x => x.UserId == id).ToList();
            var deployableApps = deployable.Select(x => x.ApplicationId).Distinct().ToList();

            var applications = db.Query<DeployTaskDto>().Where(x => x.ApplicationId == x.ApplicationId).Select(x => new {
                x.ApplicationId,
                x.ApplicationName,
                x.CurrentlyDeployedVersion,
                x.EnviromentName,
                x.EnviromentId,
                x.GroupName,
                x.Id,
                x.Name
            }).Union(db.Query<TemplatedTaskDto>().Where(x => x.ApplicationId == x.ApplicationId).Select(x => new {
                x.ApplicationId,
                x.ApplicationName,
                x.CurrentlyDeployedVersion,
                x.EnviromentName,
                x.EnviromentId,
                x.GroupName,
                x.Id,
                x.Name
            })).ToList();


            return new
            {
                Enviroments = enviroments,
                Applications = applications.GroupBy(x => x.GroupName ?? "")
                                          .Select(x => new
                                          {
                                              GroupName = x.Key,
                                              Applications = x.Where(z => deployableApps.Contains(z.ApplicationId)).GroupBy(y => y.ApplicationName).Select(y => new
                                              {
                                                  Name = y.Key,
                                                  Id = y.First().ApplicationId,
                                                  Enviroments = y.Where(z => deployable.Count(zz => zz.ApplicationId == z.ApplicationId && zz.EnviromentId == z.EnviromentId)> 0).GroupBy(z => z.EnviromentName, (envKey, ele2) => new
                                                  {
                                                      Enviroment = envKey,
                                                      Name = y.Key,
                                                      Configuration = envKey,
                                                      Id = ele2.FirstOrDefault().EnviromentId,
                                                      Versions = ele2.Select(yy => new
                                                      {
                                                          yy.Name,
                                                          yy.CurrentlyDeployedVersion
                                                      }).ToList()
                                                  }).ToList()
                                              }).OrderBy(z => z.Name)
                                          }).Where(x => x.Applications.Count() > 0)
            };

        }
    }
}