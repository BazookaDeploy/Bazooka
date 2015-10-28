namespace Web.CommandHandlers
{
    using DataAccess.Write;
    using Web.Commands;

    public class RemoveAlllowedUserFromApplicationHandler : CommandHandler<RemoveAllowedUserFromApplication>
    {
        public override void Apply(RemoveAllowedUserFromApplication command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.RemoveAllowedUser(command.EnviromentId, command.UserId);
            Repository.Save<Application>(application);
        }
    }
}
