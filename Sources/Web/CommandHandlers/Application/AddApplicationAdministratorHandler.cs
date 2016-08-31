namespace Web.CommandHandlers
{
    using DataAccess.Write;
    using Web.Commands;

    public class AddApplicationAdministratorHandler : CommandHandler<AddApplicationAdministrator>
    {
        public override void Apply(AddApplicationAdministrator command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.AddAdministrator(command.UserId);
            Repository.Save<Application>(application);
        }
    }
}
