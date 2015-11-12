namespace Web.Controllers
{
    using Commands;
    using DataAccess.Read;
    using System.Linq;
    using System.Web.Http;

    public class RemoteScriptTasksController : BaseController
    {
        public IReadContext db { get; set; }

        public RemoteScriptTaskDto Get(int id)
        {
            return db.Query<RemoteScriptTaskDto>().Single(x => x.Id == id);
        }

        [HttpPost]
        public ExecutionResult CreateRemoteScriptTask(CreateRemoteScriptTask command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult ModifyRemoteScriptTask(ModifyRemoteScriptTask command)
        {
            return Execute(command);
        }
    }
}