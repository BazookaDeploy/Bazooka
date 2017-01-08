using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateApplicationBusinessRule : BusinessRule<CreateApplication>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(CreateApplication command)
        {
            var apps = ReadContext.Query<ApplicationDto>().Count(x => x.Name == command.Name);
            if (apps > 0)
            {
                yield return "The application name must be unique";
                yield break;
            }
        }
    }
}
