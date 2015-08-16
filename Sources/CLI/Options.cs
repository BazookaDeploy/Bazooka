namespace Bazooka.CLI
{
    using CommandLine;
    using CommandLine.Text;

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

        /// <summary>
        ///     Updates an installed package
        /// </summary>
        [VerbOption("update", HelpText = "Updates application on system")]
        public UpdateOptions UpdateVerb { get; set; }

        /// <summary>
        ///     Build the helpp text
        /// </summary>
        /// <param name="verb">Verb to create the help text for</param>
        /// <returns>The help text</returns>
        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }
    }
}
