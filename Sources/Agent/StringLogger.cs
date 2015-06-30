using Bazooka.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent
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
