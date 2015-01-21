using System;
using System.Collections.Generic;
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
            // check if all needed agents are responding else abort immediately 
            List<DeployUnitDto> agents = new List<DeployUnitDto>();
            using (var dbContext = new ReadContext()) {
                agents = dbContext.DeployUnits.Where(x => x.EnviromentId == enviromentId).ToList();
            }

            var availability = agents.Select(x => x.Machine).Select(x => CheckAvailability(x));
            var results = await Task.WhenAll(availability);
            if (results.Any(x => x == null))
            {
                return new
                {
                    Success = false
                };
            }

 
            // if a version is currently deployed uninstall it
            // install the new version in this enviroment
            return new
            {
                Success = true
            };
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