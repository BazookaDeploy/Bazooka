using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Enviroment
    {
        public virtual int Id { get; set; }

        public virtual int ApplicationId { get; set; }

        public virtual string Configuration { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<DeployUnit> DeployUnits { get; set; }
    }
}