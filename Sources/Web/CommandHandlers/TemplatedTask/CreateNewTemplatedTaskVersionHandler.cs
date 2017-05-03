using DataAccess.Write;
using System.Linq;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateNewTemplatedTaskVersionHandler : CommandHandler<CreateNewTemplatedTaskVersion>
    {
        public override void Apply(CreateNewTemplatedTaskVersion command)
        {
            var task = Repository.Get<TaskTemplate>(command.TemplatedTaskId);
            task.CreateNewVersion(command.Script, command.Parameters.Select(x => new TaskTemplateParameter()
            {
                Encrypted = x.Encrypted,
                Name = x.Name,
                Optional = x.Optional,
                TaskTemplateId = task.Id
            }).ToList());
            Repository.Save(task);
        }
    }
}