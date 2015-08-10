using Agent;
using System;
using System.Collections.Generic;

namespace Bazooka.Core
{
    public class StringLogger : ILogger
    {
        public ICollection<LogEntry> Logs { get; private set; }

        public StringLogger()
        {
            Logs = new List<LogEntry>();
        }

        public void Log(string text, bool error = false)
        {
            Logs.Add(new LogEntry()
            {
                Error = error,
                TimeStamp = DateTime.UtcNow,
                Text = text
            }); ;
        }
    }
}
