using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Web.Commands;

namespace Web.Controllers
{
    public class TaskTemplateController : BaseController
    {

        public IReadContext db { get; set; }


        public ICollection<TasKTemplateDto> Get()
        {
            return db.Query<TasKTemplateDto>().ToList();
        }

        public TasKTemplateDto Get(int id)
        {
            return db.Query<TasKTemplateDto>().Single(X => X.Id == id);
        }

        [HttpGet]
        public object LastVersion(int id)
        {
            var o = db.Query<TasKTemplateDto>().Single(X => X.Id == id);
            var v =  db.Query<TaskTemplateVersionDto>().Where(X => X.TaskTemplateId == id).OrderByDescending(x => x.Version).First();
            var p = db.Query<TaskTemplateParameterDto>().Where(x => x.TaskTemplateVersionId == v.Id).ToList();

            return new
            {
                o.Name,
                o.Description,
                v.Script,
                v.Version,
                Parameters = p
            };
        }

        [HttpPost]
        public ExecutionResult CreateTemplatedTask(CreateTaskTemplate command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult ChangeDescription(ChangeTaskTemplateDescription command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult Rename(RenameTaskTemplate command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult CreateNewVersion(CreateNewTaskTemplateVersion command)
        {
            return Execute(command);
        }
    }
}