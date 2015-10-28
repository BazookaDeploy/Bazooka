using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class AddAgentToEnviromentHandler : CommandHandler<AddAgentToEnviroment>
    {
        public override void Apply(AddAgentToEnviroment command)
        {
            var env = Repository.Get<Enviroment>(command.EnviromentId);
            env.AddAgent(command.Name, command.Address);
            Repository.Save<Enviroment>(env);
        }
    }
}
