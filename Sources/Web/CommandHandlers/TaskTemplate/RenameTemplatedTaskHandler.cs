using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class RenameTaskTemplateHandler : CommandHandler<RenameTaskTemplate>
    {
        public override void Apply(RenameTaskTemplate command)
        {
            var task = Repository.Get<TaskTemplate>(command.TemplatedTaskId);
            task.Rename(command.Name);
            Repository.Save(task);
        }
    }
}