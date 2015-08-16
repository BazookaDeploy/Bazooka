namespace Bazooka.CLI
{
    using Bazooka.Core;
    using System;

    /// <summary>
    ///     Logger to console
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        ///     Logs a line of text
        /// </summary>
        /// <param name="text">Text to log</param>
        /// <param name="error">Indicates if texgt is an error</param>
        public void Log(string text, bool error = false)
        {
            Console.WriteLine(text);
        }
    }
}
