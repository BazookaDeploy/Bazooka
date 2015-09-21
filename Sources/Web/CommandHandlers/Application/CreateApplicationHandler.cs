using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateApplicationHandler : CommandHandler<CreateApplication>
    {
        public override void Apply(CreateApplication command)
        {
            Repository.Save(new Application()
            {
                Name = command.Name,                
            });
        }
    }
}
