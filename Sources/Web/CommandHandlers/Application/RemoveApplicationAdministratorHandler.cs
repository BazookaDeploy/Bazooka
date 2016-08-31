namespace Web.CommandHandlers
{
    using DataAccess.Write;
    using Web.Commands;

    public class RemoveApplicationAdministratorHandler : CommandHandler<RemoveApplicationAdministrator>
    {
        public override void Apply(RemoveApplicationAdministrator command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.RemoveAdministrator(command.UserId);
            Repository.Save<Application>(application);
        }
    }
}
