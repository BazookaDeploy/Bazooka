using DataAccess.Read;
using System.Linq;
using System.Web.Http;
using Web.Commands;

namespace Web.Controllers
{
    public class DatabaseTasksController : BaseController
    {
        public IReadContext db { get; set; }

        public DatabaseTaskDto Get(int id)
        {
            return db.Query<DatabaseTaskDto>().Single(x => x.Id == id);
        }

        [HttpPost]
        public ExecutionResult CreateDatabaseTask(CreateDatabaseTask command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult ModifyDatabaseTask(ModifyDatabaseTask command)
        {
            return Execute(command);
        }
    }
}
