namespace Bazooka.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Logger that logs to a collection of strings for later retrieval
    /// </summary>
    public class StringLogger : ILogger
    {
        /// <summary>
        ///     List of log entries
        /// </summary>
        public ICollection<LogEntry> Logs { get; private set; }

        /// <summary>
        ///     Default constructor
        /// </summary>
        public StringLogger()
        {
            Logs = new List<LogEntry>();
        }

        /// <summary>
        ///     Logs a line of text
        /// </summary>
        /// <param name="text">Text to log</param>
        /// <param name="error">If the text is an error</param>
        public void Log(string text, bool error = false)
        {
            Logs.Add(new LogEntry()
            {
                Error = error,
                TimeStamp = DateTime.UtcNow,
                Text = text
            });
        }
    }
}
