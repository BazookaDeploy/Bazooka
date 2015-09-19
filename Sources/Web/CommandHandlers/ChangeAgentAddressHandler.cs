using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
