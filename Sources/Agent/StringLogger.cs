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
        public string Logs { get; private set; }

        public StringLogger()
        {
            Logs = string.Empty;
        }

        public void Log(string text)
        {
            Logs += text + Environment.NewLine;
        }
    }
}
