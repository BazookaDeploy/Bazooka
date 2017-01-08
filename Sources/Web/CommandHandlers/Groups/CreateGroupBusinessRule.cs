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
            var groups = ReadContext.Query<GroupDto>().Where(x => x.Name == command.Name);

            if (groups.Count() > 0)
            {
                yield return "There is already another group with the same name";
                yield break;
            }
        }
    }
}
