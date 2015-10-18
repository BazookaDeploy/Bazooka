using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class SetApplicationGroupHandler : CommandHandler<SetApplicationGroup>
    {
        public override void Apply(SetApplicationGroup command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.SetApplicationGroup(command.ApplicationGroupId);
            Repository.Save<Application>(application);
        }
    }
}
