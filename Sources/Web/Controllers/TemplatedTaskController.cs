using DataAccess.Read;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Web.Commands;

namespace Web.Controllers
{
    public class TemplatedTaskController : BaseController
    {

        public IReadContext db { get; set; }


        public ICollection<TemplatedTaskDto> Get()
        {
            return db.Query<TemplatedTaskDto>().ToList();
        }

        public TemplatedTaskDto Get(int id)
        {
            return db.Query<TemplatedTaskDto>().Include(x => x.Parameters).Single(X => X.Id == id);
        }

        [HttpPost]
        public ExecutionResult CreateTemplatedTask(CreateTemplatedTask command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult RenameTemplatedTask(RenameTemplatedTask command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult Modify(ModifyTemplatedTask command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult Update(UpdateTemplatedTask command)
        {
            return Execute(command);
        }
    }
}