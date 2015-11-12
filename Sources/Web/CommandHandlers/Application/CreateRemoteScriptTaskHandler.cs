using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateRemoteScriptTaskHandler : CommandHandler<CreateRemoteScriptTask>
    {
        public override void Apply(CreateRemoteScriptTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.CreateRemoteScriptTask(command.EnviromentId, command.Script, command.Name, command.Folder, command.AgentId);
            Repository.Save<Application>(application);
        }
    }
}
