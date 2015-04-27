namespace Bazooka.Core.Dto
{
    using System.Collections.Generic;


    public class InstallDto
    {
        /// <summary>
        ///     Specifies application to be installed
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        ///     Sepcifies applciation version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Specifies instalaltion directory
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        ///     Specifies configuration to install ( used to apply transformations)
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        ///     Specifies configuration to install ( used to apply transformations)
        /// </summary>
        public string Repository { get; set; }

        /// <summary>
        ///     Specifies additional parameters to pass to scripts
        /// </summary>
        public Dictionary<string,string> AdditionalParameters { get; set; }

        /// <summary>
        ///     Optional installation script
        /// </summary>
        public virtual string InstallScript { get; set; }

        /// <summary>
        ///     Optional uninstallation script
        /// </summary>
        public virtual string UninstallScript { get; set; }

        /// <summary>
        ///     Optional configuration file
        /// </summary>
        public virtual string ConfigurationFile { get; set; }

        /// <summary>
        ///     Optional transform to apply to configuration file
        /// </summary>
        public virtual string ConfigurationTransform { get; set; }
    }
}
