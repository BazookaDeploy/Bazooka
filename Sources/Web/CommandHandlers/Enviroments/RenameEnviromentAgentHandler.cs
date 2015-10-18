using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class RenameEnviromentAgentHandler : CommandHandler<RenameEnviromentAgent>
    {

        public override void Apply(RenameEnviromentAgent command)
        {
            var env = Repository.Get<Enviroment>(command.EnviromentId);
            env.RenameAgent(command.AgentId, command.Name);
            Repository.Save<Enviroment>(env);
        }
    }
}
