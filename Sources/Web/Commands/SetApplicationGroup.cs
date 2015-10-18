using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Commands
{
    public class SetApplicationGroup : ICommand
    {
        public int ApplicationId { get; set; }

        public int? ApplicationGroupId { get; set; }
    }
}
