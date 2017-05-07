using DataAccess.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Web.Commands;

namespace Web.Controllers
{
    public class TemplatedTaskController : BaseController
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

        [HttpPost]
        public ExecutionResult CreateTemplatedTask(CreateTemplatedTask command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult CreateDescription(ChangeTemplatedTaskDescription command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult Rename(RenameTemplatedTask command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult CreateNewVersion(CreateNewTemplatedTaskVersion command)
        {
            return Execute(command);
        }
    }
}