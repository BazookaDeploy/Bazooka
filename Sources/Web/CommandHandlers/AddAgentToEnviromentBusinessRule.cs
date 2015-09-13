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
            var user = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

            if (user == null)
            {
                yield return "Only authorized users can create an enviroment";
                yield break;
            }

            if (!user.Administrator)
            {
                yield return "Only an administrator can create an enviroment";
                yield break;
            }
        }
    }
}
