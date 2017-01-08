using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;

namespace Web.Commands
{
    public class CreateEnviromentBusinessRule : BusinessRule<CreateEnviroment>
    {
        public IReadContext ReadContext { get; set; }

        public override IEnumerable<string> Validate(CreateEnviroment command)
        {
            var enviroments = ReadContext.Query<EnviromentDto>().Where(x => x.Name == command.Name);

            if (enviroments.Count() > 0)
            {
                yield return "There is already another enviroment with the same name";
                yield break;
            }
        }
    }
}
