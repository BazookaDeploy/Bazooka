using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateGroupBusinessRule : BusinessRule<CreateGroup>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(CreateGroup command)
        {
            var user = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

            if (user == null)
            {
                yield return "Only authorized users can create a group";
                yield break;
            }

            if (!user.Administrator)
            {
                yield return "Only an administrator can create a group";
                yield break;
            }


            var groups = ReadContext.Query<GroupDto>().Where(x => x.Name == command.Name);

            if (groups.Count() > 0)
            {
                yield return "There is already another group with the same name";
                yield break;
            }
        }
    }
}
