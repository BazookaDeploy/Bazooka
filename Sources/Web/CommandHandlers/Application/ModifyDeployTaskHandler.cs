using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class ModifyDeployTaskHandler : CommandHandler<ModifyDeployTask>
    {
        public override void Apply(ModifyDeployTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.ModifyDeployTask(command.DeployTaskId, command.Name, command.AgentId, command.PackageName, command.Directory, command.Repository, command.InstallScript, command.UninstallScript, command.ConfigurationFile, command.ConfigurationTransform, command.Configuration, command.AdditionalParameters);
            Repository.Save<Application>(application);
        }
    }
}
