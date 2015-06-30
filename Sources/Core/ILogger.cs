using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazooka.Core
{
    /// <summary>
    ///     Interface implemented by all loggers
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     Writes texxt to the log
        /// </summary>
        /// <param name="text">Text to write</param>
        void Log(string text, bool error = false);
    }
}
