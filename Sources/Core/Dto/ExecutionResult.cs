using Agent;
using System.Collections.Generic;
namespace Bazooka.Core.Dto
{
    public class ExecutionResult
    {
        public bool Success { get; set; }

        public string Exception {get; set;}

        public ICollection<LogEntry> Log {get; set;}
    }
}
