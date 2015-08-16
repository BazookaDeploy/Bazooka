namespace Bazooka.CLI
{
    using CommandLine;

    /// <summary>
    ///     Options for the installation of a package
    /// </summary>
    public class InstallOptions
    {
        /// <summary>
        ///     Specifies application to be installed
        /// </summary>
        [Option('a', "application", Required = true, HelpText = "Application to install")]
        public string Application { get; set; }

        /// <summary>
        ///     Sepcifies applciation version
        /// </summary>
        [Option('v', "version", Required = true, HelpText = "Application version to install")]
        public string Version { get; set; }

        /// <summary>
        ///     Specifies instalaltion directory
        /// </summary>
        [Option('d', "directory", Required = true, HelpText = "Directory where to install application")]
        public string Directory { get; set; }

        /// <summary>
        ///     Specifies configuration to install ( used to apply transformations)
        /// </summary>
        [Option('c', "configuration", Required = true, HelpText = "Configuration to use to install application")]
        public string Configuration { get; set; }

        /// <summary>
        ///     Specifies configuration to install ( used to apply transformations)
        /// </summary>
        [Option('r', "repository", Required = true, HelpText = "Repository to use to install the application")]
        public string Repository { get; set; }

        /// <summary>
        ///     Specifies additional parameters to pass to scripts
        /// </summary>
        [OptionArray('p', "parameters", Required = true, HelpText = "Parameters to pass to scripts")]
        public string[] AdditionalParameters { get; set; }
    }
}
