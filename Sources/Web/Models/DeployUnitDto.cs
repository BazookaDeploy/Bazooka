using System.ComponentModel.DataAnnotations;
namespace Web.Models
{
    public class DeployUnitDto
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Machine { get; set; }

        public virtual string PackageName { get; set; }

        public virtual string Directory { get; set; }
    }
}