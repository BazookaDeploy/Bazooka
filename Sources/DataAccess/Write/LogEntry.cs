using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Write
{
    public class LogEntry
    {
        public virtual int Id { get; set; }

        public virtual DateTime TimeStamp { get; set; }

        public virtual string Text { get; set; }

        public virtual bool Error { get; set; }

        public virtual int DeploymentId { get; set; }
    }
}
