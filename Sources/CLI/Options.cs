using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazooka.CLI
{

    /// <summary>
    ///     Program options
    /// </summary>
    public class Options
    {
        /// <summary>
        ///     Installs a new app
        /// </summary>
        [VerbOption("install",HelpText="Install application on system")]
        public InstallOptions InstallVerb { get; set; }

        /// <summary>
        ///     Removes an pap
        /// </summary>
        [VerbOption("blast", HelpText = "Removes application on system")]
        public BlastOptions BlastVerb { get; set; }

        /// <summary>
        ///     Uninstalls an installed app
        /// </summary>
        [VerbOption("uninstall", HelpText = "Uninstall application on system")]
        public UninstallOptions UninstallVerb {get; set;}

        [VerbOption("update", HelpText = "Updates application on system")]
        public UpdateOptions UpdateVerb { get; set; }



        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }
    }
}
