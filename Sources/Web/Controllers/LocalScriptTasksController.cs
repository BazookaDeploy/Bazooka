namespace Web.Controllers
{
    using Commands;
    using DataAccess.Read;
    using System.Linq;
    using System.Web.Http;

    public class LocalScriptTasksController : BaseController
    {
        public IReadContext db { get; set; }

        public LocalScriptTaskDto Get(int id)
        {
            return db.Query<LocalScriptTaskDto>().Single(x => x.Id == id);
        }

        [HttpPost]
        public ExecutionResult CreateLocalScriptTask(CreateLocalScriptTask command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult ModifyLocalScriptTask(ModifyLocalScriptTask command)
        {
            return Execute(command);
        }
    }
}

