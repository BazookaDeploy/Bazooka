namespace Agent
{
    using System;

    public class LogEntry
    {
        public DateTime TimeStamp { get; set; }

        public string Text { get; set; }

        public bool Error { get; set; }
    }
}
