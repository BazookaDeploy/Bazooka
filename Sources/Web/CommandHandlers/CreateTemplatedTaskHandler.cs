namespace Web.CommandHandlers
{
    using DataAccess.Write;
    using Web.Commands;

    public class CreateTemplatedTaskHandler : CommandHandler<CreateTemplatedTask>
    {
        public override void Apply(CreateTemplatedTask command)
        {
            var task = new TaskTemplate();
            task.Create(command.Name, command.Description);
            Repository.Save(task);
        }
    }
}