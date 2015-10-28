using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class ModifyRemoteScriptTaskHandler : CommandHandler<ModifyRemoteScriptTask>
    {
        public override void Apply(ModifyRemoteScriptTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.ModifyRemoteScriptTask(command.RemoteScriptTaskId, command.EnviromentId, command.Script, command.Name, command.Folder, command.AgentId);
            Repository.Save<Application>(application);
        }
    }
}
