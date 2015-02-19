using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazooka.Core.Commands
{
    public class DeployApplication : ICommand
    {
        public int DeploymentId { get; set; }
    }
}
