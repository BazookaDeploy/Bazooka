using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class DeployUnit
    {
        public int Id { get; set; }

        public int EnviromentId { get; set; }

        public virtual Enviroment Enviroment { get; set; }

        public string Name { get; set; }

        public string Machine { get; set; }

        public string PackageName { get; set; }

        public string Directory { get; set; }

        public ICollection<Parameter> AdditionalParameters { get; set; }
    }

    public class Parameter
    {
        public int ParameterId { get; set; }

        public int DeployUnitId { get; set; }

        public virtual DeployUnit DeployUnit { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}