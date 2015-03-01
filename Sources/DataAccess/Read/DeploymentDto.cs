using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Read
{
    public class DeploymentDto
    {
        public virtual int Id { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Configuration { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual string Log { get; set; }

        public virtual Status Status { get; set; }

        public virtual string Version { get; set; }
    }
}
