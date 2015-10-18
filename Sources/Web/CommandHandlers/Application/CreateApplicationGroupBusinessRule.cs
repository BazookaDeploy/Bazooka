using DataAccess.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class SetApplicationGroupBusinessRule : BusinessRule<SetApplicationGroup>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(SetApplicationGroup command)
        {
            var user = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

            if (user == null)
            {
                yield return "Only authorized users can create an application group";
                yield break;
            }

            if (!user.Administrator)
            {
                yield return "Only an administrator can create an application group";
                yield break;
            }
        }
    }
}
