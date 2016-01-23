using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Web.Commands;
using Web.Commands.Application;

namespace Web.Controllers
{
    public class TasksController : BaseController
    {
        private ReadContext db = new ReadContext();

        public ICollection<TaskDto> Get(int enviromentId, int applicationId)
        {
            return db.Tasks.Where(x => x.EnviromentId == enviromentId & x.ApplicationId == applicationId).ToList();
        }

        [HttpPost]
        public ExecutionResult DeleteTask(DeleteTask command)
        {
            return Execute(command);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
