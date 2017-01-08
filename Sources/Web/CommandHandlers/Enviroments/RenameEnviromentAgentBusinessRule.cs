using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class RenameEnviromentAgentBusinessRule : BusinessRule<RenameEnviromentAgent>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(RenameEnviromentAgent command)
        {
            if (ReadContext.Query<AgentDto>().Count(x => x.Name == command.Name) > 0)
            {
                yield return "The agent name must be unique";
                yield break;
            }
        }
    }
}
