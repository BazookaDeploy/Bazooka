using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class AddUserToGroupBusinessRule : BusinessRule<AddUserToGroup>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(AddUserToGroup command)
        {
            var user = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

            if (user == null)
            {
                yield return "Only authorized users can add a user to a group";
                yield break;
            }

            if (!user.Administrator)
            {
                yield return "Only an administrator can add a user to a group";
                yield break;
            }
        }
    }
}
