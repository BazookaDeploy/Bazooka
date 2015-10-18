using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateApplicationGroupHandler : CommandHandler<CreateApplicationGroup>
    {
        public override void Apply(CreateApplicationGroup command)
        {
            Repository.Save<ApplicationGroup>(new ApplicationGroup()
            {
                Name = command.Name
            });
        }
    }
}
