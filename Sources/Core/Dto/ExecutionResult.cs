namespace Bazooka.Core.Dto
{
    using System.Collections.Generic;

    /// <summary>
    ///     REsult of a task execution
    /// </summary>
    public class ExecutionResult
    {
        /// <summary>
        ///     Success of the task
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     Optional exception occurred
        /// </summary>
        public string Exception {get; set;}

        /// <summary>
        ///     List of logs
        /// </summary>
        public ICollection<LogEntry> Log {get; set;}
    }
}
