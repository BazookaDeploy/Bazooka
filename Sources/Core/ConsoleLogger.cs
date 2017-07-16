using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazooka.Core
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string text, bool error = false)
        {
            if (error)
            {
                Console.Error.WriteLine(text);
            }
            else
            {
                Console.WriteLine(text);
            }
        }
    }
}
