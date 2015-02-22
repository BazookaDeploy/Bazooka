using Bazooka.Core.Commands;
using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Web.Controllers
{
    public class DeployController : ApiController
    {
        [HttpGet]
        public void Deploy(int enviromentId, string version)
        {
            using (var session = WebApiApplication.Store.OpenSession())
            {
                var deploy = new Deployment()
                {
                    EnviromentId = enviromentId,
                    Status = Status.Queud,
                    Version = version
                };

                session.Save(deploy);
                session.Flush();

                WebApiApplication.Bus.Send("bazooka.controller", new DeployApplication()
                {
                    DeploymentId = deploy.Id
                });
            };
        }
    }
}