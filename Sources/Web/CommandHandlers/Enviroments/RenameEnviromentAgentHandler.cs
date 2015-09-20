using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
