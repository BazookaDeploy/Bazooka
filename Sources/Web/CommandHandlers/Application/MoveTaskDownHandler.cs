using DataAccess.Write;
using Web.Commands;
using Web.Commands.Application;

namespace Web.CommandHandlers
{
    public class MoveTaskDownHandler : CommandHandler<MoveTaskDown>
    {
        public override void Apply(MoveTaskDown command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.MoveDown(command.Position, command.EnviromentId);
            Repository.Save<Application>(application);
        }
    }
}