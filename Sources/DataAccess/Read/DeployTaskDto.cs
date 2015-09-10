using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DataAccess.Read
{
    public class DeployTaskDto
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual int ApplicationId { get; set; }

        public virtual string Name { get; set; }

        public virtual string ApplicationName { get; set; }

        public virtual string EnviromentName { get; set; }

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

        public virtual string Configuration { get; set; }

        public virtual ICollection<DeployTaskParameterDto> Parameters { get; set; }
    }
}