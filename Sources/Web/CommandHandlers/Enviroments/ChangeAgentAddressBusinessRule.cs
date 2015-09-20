using DataAccess.Read;
using Web.Commands;
using System.Linq;

namespace Web.CommandHandlers
{
    public class ChangeAgentAddressBusinessRule : BusinessRule<ChangeAgentAddress>
    {
        public IReadContext ReadContext { get; set; }

        public override System.Collections.Generic.IEnumerable<string> Validate(ChangeAgentAddress command)
        {
            var user = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

            if (user == null)
            {
                yield return "Only authorized users can change an agent address";
                yield break;
            }

            if (!user.Administrator)
            {
                yield return "Only an administrator can change an agent address";
                yield break;
            }
        }
    }
}
