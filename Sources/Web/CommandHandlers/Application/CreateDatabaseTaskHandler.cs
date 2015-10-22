using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateDatabaseTaskHandler : CommandHandler<CreateDatabaseTask>
    {
        public override void Apply(CreateDatabaseTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.AddDatabaseTask(command.Name, command.ConnectionString, command.Package, command.DatabaseName, command.EnviromentId, command.Repository, command.AgentId);
            Repository.Save<Application>(application);
        }
    }
}
