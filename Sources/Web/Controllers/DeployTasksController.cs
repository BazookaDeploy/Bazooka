using DataAccess.Read;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Web.Commands;

namespace Web.Controllers
{
    public class DeployTasksController : BaseController
    {
        public IReadContext db { get; set; }

        public DeployTaskDto Get(int id)
        {
            return db.Query<DeployTaskDto>().Single(x => x.Id == id);
        }

        [HttpPost]
        public ExecutionResult CreateDeployTask(CreateDeployTask command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult ModifyDeployTask(ModifyDeployTask command)
        {
            return Execute(command);
        }

        [HttpGet, Route("api/DeployTasks/test")]
        public object Test(string url)
        {
            var client = new HttpClient();
            var res = client.GetAsync(url + "/api/health/ping").Result;
            return new
            {
                Success = res.IsSuccessStatusCode
            };
        }
    }
}