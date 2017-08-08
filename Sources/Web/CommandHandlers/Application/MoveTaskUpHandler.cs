using DataAccess.Write;
using Web.Commands;
using Web.Commands.Application;

namespace Web.CommandHandlers
{
    public class MoveTaskUpHandler : CommandHandler<MoveTaskUp>
    {
        public override void Apply(MoveTaskUp command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.MoveUp(command.Position, command.EnviromentId);
            Repository.Save<Application>(application);
        }
    }
}