using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Write
{
    public class DeployUnit
    {
        public virtual int Id { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Machine { get; set; }

        public virtual string PackageName { get; set; }

        public virtual string Directory { get; set; }

        public virtual string Repository { get; set; }

        public virtual string CurrentlyDeployedVersion { get; set; }

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

        /// <summary>
        ///     Additional deploy parameters to pass to the script
        /// </summary>
        public virtual IList<Parameter> AdditionalParameters { get; set; }

        /// <summary>
        ///     
        /// </summary>
        public virtual IList<Deployment> Logs { get; set; }
    }

    public class Parameter
    {
        public virtual int ParameterId { get; set; }

        public virtual int DeployUnitId { get; set; }

        public virtual string Key { get; set; }

        public virtual string Value { get; set; }
    }
}