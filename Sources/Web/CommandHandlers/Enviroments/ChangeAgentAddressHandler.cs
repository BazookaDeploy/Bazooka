using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class ChangeAgentAddressHandler : CommandHandler<ChangeAgentAddress>
    {
        public override void Apply(ChangeAgentAddress command)
        {
            var env = Repository.Get<Enviroment>(command.EnviromentId);
            env.ChangeAgentAddress(command.AgentId, command.Address);
            Repository.Save<Enviroment>(env);
        }
    }
}
