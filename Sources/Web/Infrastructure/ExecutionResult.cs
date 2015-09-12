using System.Collections.Generic;

namespace Web.Commands
{
    public class ExecutionResult
    {
        public bool Success { get; set; }

        public ICollection<string> Errors { get; set; }
    }
}
