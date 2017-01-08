using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class AddAgentToEnviromentBusinessRule : BusinessRule<AddAgentToEnviroment>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(AddAgentToEnviroment command)
        {
            var agents = ReadContext.Query<AgentDto>().Count(x => x.Name == command.Name);
            if (agents > 0)
            {
                yield return "The agent name must be unique";
                yield break;
            }
        }
    }
}
