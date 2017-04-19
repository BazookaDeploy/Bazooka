namespace DataAccess.Write
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    ///     A parameter for a task template
    /// </summary>
    public class TaskTemplateParameter
    {
        /// <summary>
        ///     Identifier
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     The identifier of the task template
        /// </summary>
        public virtual int TaskTemplateId { get; set; }

        /// <summary>
        ///     name of the parameter
        /// </summary>
        public virtual string Name { get; set; }

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
