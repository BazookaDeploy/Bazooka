using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Web.Mapping;
using Web.Models;

namespace Web.Controllers
{
    public class DeployController : ApiController
    {

        public async Task<object> Deploy(int enviromentId, string version)
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
            };

            try
            {
                var url = ConfigurationManager.AppSettings["controllerUrl"];
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync(url).Result;
                    return new
                    {
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                return new
                {
                    Success = false
                };
            }
        }

        public async Task<string> CheckAvailability(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url + "/Health/ping");
                return (await response.Content.ReadAsAsync<string>());
            }
        }
    }
}