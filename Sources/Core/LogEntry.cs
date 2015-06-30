namespace Agent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LogEntry
    {
        public DateTime TimeStamp { get; set; }

        public string Text { get; set; }

        public bool Error { get; set; }
    }
}
