using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class DeployLog
    {
        public virtual int Id { get; set; }

        public virtual int DeployUnitId { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual string Log { get; set; }
    }
}