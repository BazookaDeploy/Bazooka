using DataAccess.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class RenameEnviromentAgentBusinessRule : BusinessRule<RenameEnviromentAgent>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(RenameEnviromentAgent command)
        {
            var user = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

            if (user == null)
            {
                yield return "Only authorized users can change an agent name";
                yield break;
            }

            if (!user.Administrator)
            {
                yield return "Only an administrator can change an agent name";
                yield break;
            }

            if (ReadContext.Query<AgentDto>().Count(x => x.Name == command.Name) > 0)
            {
                yield return "The agent name must be unique";
                yield break;
            }
        }
    }
}
