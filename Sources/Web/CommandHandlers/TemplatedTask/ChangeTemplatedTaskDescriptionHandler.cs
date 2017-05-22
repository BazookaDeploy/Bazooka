using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class ChangeTemplatedTaskDescriptionHandler : CommandHandler<ChangeTaskTemplateDescription>
    {
        public override void Apply(ChangeTaskTemplateDescription command)
        {
            var task = Repository.Get<TaskTemplate>(command.TemplatedTaskId);
            task.ChangeDescription(command.Description);
            Repository.Save(task);
        }
    }
}