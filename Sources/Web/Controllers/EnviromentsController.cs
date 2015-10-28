namespace Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess.Read;
    using Commands;
    using System.Web.Http;

    [Authorize]
    public class EnviromentsController : BaseController
    {
        public IReadContext ReadContext { get; set; }

        public ICollection<EnviromentDto> Get()
        {
            return ReadContext.Query<EnviromentDto>().ToList();
        }

        public ExecutionResult Create(CreateEnviroment command)
        {
            return Execute(command);
        }

        public ExecutionResult AddAgent(AddAgentToEnviroment command)
        {
            return Execute(command);
        }
    }
}
