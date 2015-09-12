using DataAccess.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Commands
{
    public class CreateEnviromentBusinessRule : BusinessRule<CreateEnviroment>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(CreateEnviroment command)
        {
            if (command.CurrentUserId == Guid.Empty)
            {
                yield return "Only authorized users can create an enviroment";
                yield break;
            }

            var user = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

            if (user == null)
            {
                yield return "Only authorized users can create an enviroment";
                yield break;
            }

            if (!user.Administrator)
            {
                yield return "Only an administrator can create enviroments";
                yield break;
            }

            var enviroments = ReadContext.Query<EnviromentDto>().Where(x => x.Name == command.Name);

            if (enviroments.Count() > 0)
            {
                yield return "There is already another enviroment with the same name";
                yield break;
            }
        }
    }
}
