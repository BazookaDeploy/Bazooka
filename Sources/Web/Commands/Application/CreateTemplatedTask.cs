using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Commands
{
    public class CreateTemplatedTask : ICommand, ICanBeRunByApplicationAdministrator
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int EnviromentId { get; set; }
        public virtual int ApplicationId { get; set; }
        public virtual int AgentId { get; set; }

        public ICollection<TemplatedTaskParameter> Parameters { get; set; }
    }

    public class TemplatedTaskParameter
    {
        /// <summary>
        ///     name of the parameter
        /// </summary>
        public virtual int ParameterId { get; set; }

        /// <summary>
        ///     name of the parameter
        /// </summary>
        public virtual string Value { get; set; }
    }
}