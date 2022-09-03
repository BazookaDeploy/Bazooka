using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class ModifyDatabaseTaskHandler : CommandHandler<ModifyDatabaseTask>
    {
        public override void Apply(ModifyDatabaseTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.ModifyDatabaseTask(command.DatabaseTaskId, command.Name, command.ConnectionString, command.Package, command.DatabaseName, command.Repository, command.AgentId, command.Partial);
            Repository.Save<Application>(application);
        }
    }
}
