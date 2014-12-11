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

        public string Name { get; set; }

        public string Machine { get; set; }

        public string PackageName { get; set; }

        public string Directory { get; set; }

        public Dictionary<string, string> AdditionalParameters { get; set; }
    }
}