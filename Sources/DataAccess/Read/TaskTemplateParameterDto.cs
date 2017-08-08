using System.ComponentModel.DataAnnotations;

namespace DataAccess.Read
{
    public class TaskTemplateParameterDto
    {
        [Key]
        public virtual int Id { get; set; }

        /// <summary>
        ///     The identifier of the task template
        /// </summary>
        public virtual int TaskTemplateVersionId { get; set; }

        /// <summary>
        ///     name of the parameter
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Description of the parameter
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        ///     Indicates if a parameter is optional
        /// </summary>
        public virtual bool Optional { get; set; }

        /// <summary>
        ///     Indicates if a parameter has to be Encrypted
        /// </summary>
        public virtual bool Encrypted { get; set; }
    }
}