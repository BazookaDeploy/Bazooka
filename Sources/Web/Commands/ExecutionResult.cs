using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Commands
{
    public class ExecutionResult
    {
        public bool Success { get; set; }

        public ICollection<string> Errors { get; set; }
    }
}
