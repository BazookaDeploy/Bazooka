namespace Web.CommandHandlers
{
    using DataAccess.Write;
    using Web.Commands;

    public class UpdateTemplatedTaskHandler : CommandHandler<UpdateTemplatedTask>
    {
        public override void Apply(UpdateTemplatedTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.UpdateTemplatedTask(command.Id, command.Version);
            Repository.Save<Application>(application);
        }
    }
}