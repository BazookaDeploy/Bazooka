using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class ModifyLocalScriptTaskHandler : CommandHandler<ModifyLocalScriptTask>
    {
        public override void Apply(ModifyLocalScriptTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.ModifyLocalScriptTask(command.LocalScriptTaskId, command.EnviromentId, command.Script, command.Name);
            Repository.Save<Application>(application);
        }
    }
}
