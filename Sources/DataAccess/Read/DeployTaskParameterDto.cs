using System.ComponentModel.DataAnnotations;

namespace DataAccess.Read
{
    public class DeployTaskParameterDto
    {
        [Key]
        public virtual int ParameterId { get; set; }

        public virtual int DeployTaskId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Value { get; set; }

        public virtual bool Encrypted { get; set; }
    }
}