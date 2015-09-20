using DataAccess.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class RemoveUserFromGroupBusinessRule : BusinessRule<RemoveUserFromGroup>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(RemoveUserFromGroup command)
        {
            var user = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

            if (user == null)
            {
                yield return "Only authorized users can remove a user from a group";
                yield break;
            }

            if (!user.Administrator)
            {
                yield return "Only an administrator can remove a user from a group";
                yield break;
            }
        }
    }
}
