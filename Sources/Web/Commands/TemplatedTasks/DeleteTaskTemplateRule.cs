using DataAccess.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Commands
{
    public class DeleteTaskTemplateRule : BusinessRule<DeleteTaskTemplate>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(DeleteTaskTemplate command)
        {
            var used = ReadContext.Query<TemplatedTaskDto>().Count(x => x.TaskTemplateId == command.TemplatedTaskId);

            if(used > 0)
            {
                yield return "You cannot delete a used task template";
            }
        }
    }
}