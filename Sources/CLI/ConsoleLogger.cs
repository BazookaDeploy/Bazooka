using Bazooka.Core;
using System;

namespace Bazooka.CLI
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string text, bool error = false)
        {
            Console.WriteLine(text);
        }
    }
}
