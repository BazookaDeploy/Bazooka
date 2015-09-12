namespace Web.Controllers
{
    using DataAccess.Read;
    using DataAccess.Write;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Commands;

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
    }
}
