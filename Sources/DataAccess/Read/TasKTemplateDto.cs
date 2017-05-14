using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DataAccess.Read
{
    public class TasKTemplateDto
    {
        [Key]
        /// <summary>
        ///     Template identifier
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     name of the template
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     An optional description of the template
        /// </summary>
        public virtual string Description { get; set; }
    }
}