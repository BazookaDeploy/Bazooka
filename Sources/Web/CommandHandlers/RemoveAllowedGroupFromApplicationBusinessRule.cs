using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class RemoveAllowedGroupFromApplicationBusinessRule : BusinessRule<RemoveAllowedGroupFromApplication>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(RemoveAllowedGroupFromApplication command)
        {
            var user = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

            if (user == null)
            {
                yield return "Only authorized users can remove an allowed group";
                yield break;
            }

            if (!user.Administrator)
            {
                yield return "Only an administrator can remove an allowed group";
                yield break;
            }
        }
    }
}
