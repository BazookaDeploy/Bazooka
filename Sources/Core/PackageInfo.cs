namespace Bazooka.Core
{
    /// <summary>
    ///     package informations
    /// </summary>
    public class PackageInfo
    {
        /// <summary>
        ///     Name of the package
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Version of installed package
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Installed Configuration (Debug, Test,Production, UAT,...)
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        ///     Directory where the package was installed ( necessary to remove installed files)
        /// </summary>
        public string InstallationDirectory { get; set; }
    }
}
