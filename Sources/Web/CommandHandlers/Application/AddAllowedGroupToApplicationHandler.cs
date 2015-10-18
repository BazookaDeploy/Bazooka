using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class AddAllowedGroupToApplicationHandler : CommandHandler<AddAllowedGroupToApplication>
    {
        public override void Apply(AddAllowedGroupToApplication command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.AddAllowedGroup(command.EnviromentId, command.GroupId);
            Repository.Save<Application>(application);
        }
    }
}
