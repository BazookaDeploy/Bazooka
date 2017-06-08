namespace Web.CommandHandlers
{
    using DataAccess.Write;
    using Web.Commands;

    public class RenameTemplatedTaskHandler : CommandHandler<RenameTemplatedTask>
    {
        public override void Apply(RenameTemplatedTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.RenameTemplatedTask(command.Id, command.EnviromentId, command.Name);
            Repository.Save<Application>(application);
        }
    }
}