using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateTemplatedTaskRule : BusinessRule<CreateTemplatedTask>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(CreateTemplatedTask command)
        {

            var v = ReadContext.Query<TaskTemplateVersionDto>().Where(X => X.Id == command.TaskVersionId).OrderByDescending(x => x.Version).First();
            var p = ReadContext.Query<TaskTemplateParameterDto>().Where(x => x.TaskTemplateVersionId == v.Id).ToList();

            if(p.Any(x => !x.Optional && !command.Parameters.Any(z => z.TaskTemplateParameterId == x.Id))){
                yield return "Parameters missing: " + string.Join(", ", p.Where(x => !x.Optional && !command.Parameters.Any(z => z.TaskTemplateParameterId == x.Id))); 
            }
        }
    }
}
