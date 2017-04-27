using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Write
{
    public class TaskTemplateVersion
    {
        /// <summary>
        ///     Template identifier
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     The identifier of the task template
        /// </summary>
        public virtual int TaskTemplateId { get; set; }

        /// <summary>
        ///     Version of the task template
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        ///     The powershell script to execute the template
        /// </summary>
        public virtual string Script { get; set; }

        /// <summary>
        ///     List of parameters
        /// </summary>
        public virtual IList<TaskTemplateParameter> Parameters { get; set; }
    }
}
