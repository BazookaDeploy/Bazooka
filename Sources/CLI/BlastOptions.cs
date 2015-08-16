namespace Bazooka.CLI
{
    using CommandLine;

    /// <summary>
    ///     Options for the blast command
    /// </summary>
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
        [OptionArray('p', "parameters", Required = false, HelpText = "Parameters to pass to scripts")]
        public string[] AdditionalParameters { get; set; }
    }
}
