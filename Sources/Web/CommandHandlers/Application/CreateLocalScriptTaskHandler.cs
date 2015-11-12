using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateLocalScriptTaskHandler : CommandHandler<CreateLocalScriptTask>
    {
        public override void Apply(CreateLocalScriptTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.CreateLocalScriptTask(command.EnviromentId, command.Script, command.Name);
            Repository.Save<Application>(application);
        }
    }
}
