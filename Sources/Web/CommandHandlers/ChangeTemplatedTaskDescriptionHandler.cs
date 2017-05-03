using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class ChangeTemplatedTaskDescriptionHandler : CommandHandler<ChangeTemplatedTaskDescription>
    {
        public override void Apply(ChangeTemplatedTaskDescription command)
        {
            var task = Repository.Get<TaskTemplate>(command.TemplatedTaskId);
            task.ChangeDescription(command.Description);
            Repository.Save(task);
        }
    }
}