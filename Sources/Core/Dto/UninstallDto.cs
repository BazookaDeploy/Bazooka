namespace Bazooka.Core.Dto
{
    using System.Collections.Generic;

    /// <summary>
    ///     Uninstall dto
    /// </summary>
    public class UninstallDto
    {
        /// <summary>
        ///     Specifies application to be uninstalled
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
        ///     Specifies additional parameters to pass to scripts
        /// </summary>
        public Dictionary<string,string> AdditionalParameters { get; set; }

        public virtual string UninstallScript { get; set; }
    }
}
