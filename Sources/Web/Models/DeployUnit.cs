using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class DeployUnit
    {
        public virtual int Id { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Machine { get; set; }

        public virtual string PackageName { get; set; }

        public virtual string Directory { get; set; }

        public virtual IList<Parameter> AdditionalParameters { get; set; }

        public virtual IList<DeployLog> Logs { get; set; }
    }

    public class Parameter
    {
        public virtual int ParameterId { get; set; }

        public virtual int DeployUnitId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Value { get; set; }
    }
}