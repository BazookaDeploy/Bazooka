namespace Web.Controllers
{
    using DataAccess.Read;
    using System;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using System.Linq;
    using Web.Commands;
    using System.Collections.Generic;
    public class AgentsController : BaseController
    {
        public IReadContext ReadContext { get; set; }

        public AgentDto Get(int id)
        {
            return ReadContext.Query<AgentDto>().Single(x => x.Id == id);
        }

        [HttpGet]
        public ICollection<AgentDto> AgentsByEnviroment(int id)
        {
            return ReadContext.Query<AgentDto>().Where(x => x.EnviromentId == id).ToList();
        }

        [HttpGet]
        public object Test(string url)
        {
            var client = new HttpClient();
            var res = client.GetAsync(url + "/api/health/ping").Result;
            return new
            {
                Success = res.IsSuccessStatusCode
            };
        }

        [HttpPost]
        public object Update(string agent)
        {
            var file = HttpContext.Current.Request.Files[0];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(agent);
                return client.PostAsync("/api/update/update", new StreamContent(file.InputStream)).Result;
            }
        }

        [HttpPost]
        public ExecutionResult ChangeAddress(ChangeAgentAddress command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult Rename(RenameEnviromentAgent command)
        {
            return Execute(command);
        }
    }
}
