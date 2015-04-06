using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DataAccess.Read
{
    public class DeployUnitDto
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual string Name { get; set; }

        public virtual string ApplicationName { get; set; }

        public virtual string EnviromentName { get; set; }

        public virtual string Machine { get; set; }

        public virtual string PackageName { get; set; }

        public virtual string Directory { get; set; }

        public virtual string Repository { get; set; }

        public virtual string CurrentlyDeployedVersion { get; set; }

        public virtual ICollection<DeployUnitParameterDto> Parameters { get; set; }
    }
}