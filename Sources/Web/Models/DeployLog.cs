using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class DeployLog
    {
        public int Id { get; set; }

        public int DeployUnitId { get; set; }

        public DateTime StartDate { get; set; }

        public string Log { get; set; }
    }
}