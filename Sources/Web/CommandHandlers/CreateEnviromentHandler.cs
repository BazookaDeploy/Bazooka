using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateEnviromentHandler : CommandHandler<CreateEnviroment>
    {
        public override void Apply(CreateEnviroment command)
        {
            Repository.Save(new Enviroment() { Name = command.Name });
        }
    }
}
