using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazooka.CLI
{
    public class BlastOptions
    {
        /// <summary>
        ///     Specifies instalaltion directory
        /// </summary>
        [Option('d', "directory", Required = true, HelpText = "Directory where to install application")]
        public string Directory { get; set; }

        /// <summary>
        ///     Specifies additional parameters to pass to scripts
        /// </summary>
        [Option('p', "parameters", Required = false, HelpText = "Parameters to pass to scripts")]
        public string AdditionalParameters { get; set; }
    }
}
