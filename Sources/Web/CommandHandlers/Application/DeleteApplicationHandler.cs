namespace Web.CommandHandlers
{
    using DataAccess.Write;
    using Web.Commands;

    public class DeleteApplicationHandler : CommandHandler<DeleteApplication>
    {
        public override void Apply(DeleteApplication command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.Delete();
            Repository.Save<Application>(application);
        }
    }
}
