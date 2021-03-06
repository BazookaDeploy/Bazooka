﻿namespace Bazooka.CLI
{
    using CommandLine;

    /// <summary>
    ///     Options to uninstall a package
    /// </summary>
    public class UninstallOptions
    {
        /// <summary>
        ///     Specifies application to be uninstalled
        /// </summary>
        [Option('a', "application", Required = true, HelpText = "Application to uninstall")]
        public string Application { get; set; }

        /// <summary>
        ///     Sepcifies applciation version
        /// </summary>
        [Option('v', "version", Required = true, HelpText = "Application version to uninstall")]
        public string Version { get; set; }

        /// <summary>
        ///     Specifies instalaltion directory
        /// </summary>
        [Option('d', "directory", Required = true, HelpText = "Directory where to uninstall application")]
        public string Directory { get; set; }

        /// <summary>
        ///     Specifies configuration to install ( used to apply transformations)
        /// </summary>
        [Option('c', "configuration", Required = true, HelpText = "Configuration to use to uninstall application")]
        public string Configuration { get; set; }

        /// <summary>
        ///     Specifies additional parameters to pass to scripts
        /// </summary>
        [OptionArray('p', "parameters", Required = true, HelpText = "Parameters to pass to scripts")]
        public string[] AdditionalParameters { get; set; }
    }
}
