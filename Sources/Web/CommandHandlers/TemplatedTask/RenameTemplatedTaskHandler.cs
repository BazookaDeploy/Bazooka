using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class RenameTemplatedTaskHandler : CommandHandler<RenameTemplatedTask>
    {
        public override void Apply(RenameTemplatedTask command)
        {
            var task = Repository.Get<TaskTemplate>(command.TemplatedTaskId);
            task.Rename(command.Name);
            Repository.Save(task);
        }
    }
}