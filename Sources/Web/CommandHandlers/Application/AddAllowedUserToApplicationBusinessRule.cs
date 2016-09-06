using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class AddAllowedUserToApplicationBusinessRule : BusinessRule<AddAllowedUserToApplication>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(AddAllowedUserToApplication command)
        {
            var user = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

            if (user == null)
            {
                yield return "Only authorized users can add an allowed user";
                yield break;
            }

            if (!user.Administrator)
            {
                if (ReadContext.Query<ApplicationAdministratorDto>().Where(x => x.ApplicationId == command.ApplicationId && x.UserId == command.CurrentUserId.ToString()).Count() == 0)
                {
                    yield return "Only an administrator can add an allowed group";
                    yield break;
                }
            }
        }
    }
}
