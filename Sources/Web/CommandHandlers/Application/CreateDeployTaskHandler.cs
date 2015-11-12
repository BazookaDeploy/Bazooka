using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateDeployTaskHandler : CommandHandler<CreateDeployTask>
    {
        public override void Apply(CreateDeployTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.AddDeployTask(command.EnviromentId, command.Name, command.AgentId, command.PackageName, command.Directory, command.Repository, command.Configuration, command.AdditionalParameters);
            Repository.Save<Application>(application);
        }
    }
}
