using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Commands
{
    public class AddAllowedUserToApplication : ICommand
    {
        public int ApplicationId { get; set; }

        public int EnviromentId { get; set; }

        public Guid UserId { get; set; }
    }
}
