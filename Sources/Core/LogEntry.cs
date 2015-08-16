namespace Bazooka.Core
{
    using System;

    /// <summary>
    ///     Entry for the logs
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        ///     Timestamp of the log
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        ///     Texxt of the log
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Indicates if log entry is an error
        /// </summary>
        public bool Error { get; set; }
    }
}
