using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class DeleteTaskTemplateHandler : CommandHandler<DeleteTaskTemplate>
    {
        public override void Apply(DeleteTaskTemplate command)
        {
            var task = Repository.Get<TaskTemplate>(command.TemplatedTaskId);
            task.Delete();
            Repository.Save(task);
        }
    }
}