namespace Web.Controllers
{
    using Commands;
    using DataAccess.Read;
    using DataAccess.Write;
    using System.Linq;
    using System.Web.Http;

    public class MailTasksController : BaseController
    {
        public IReadContext db { get; set; }

        public MailTaskDto Get(int id)
        {
            return db.Query<MailTaskDto>().Single(x => x.Id == id);
        }

        [HttpPost]
        public ExecutionResult CreateMailTask(CreateMailTask command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult ModifyMailTask(ModifyMailTask command)
        {
            return Execute(command);
        }
    }
}
