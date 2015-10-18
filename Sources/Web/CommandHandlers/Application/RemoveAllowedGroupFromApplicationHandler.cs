using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class RemoveAllowedGroupFromApplicationHandler : CommandHandler<RemoveAllowedGroupFromApplication>
    {
        public override void Apply(RemoveAllowedGroupFromApplication command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.RemoveAllowedGroup(command.EnviromentId, command.GroupId);
            Repository.Save<Application>(application);
        }
    }
}
