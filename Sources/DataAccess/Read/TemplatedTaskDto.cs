using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Read
{
    public class TemplatedTaskDto
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int EnviromentId { get; set; }

        public virtual int ApplicationId { get; set; }

        public virtual int AgentId { get; set; }


        public virtual int Position { get; set; }

        public virtual int LastKnownVersion { get; set; }

        public virtual string AgentName { get; set; }

        public virtual string Script { get; set; }

        public virtual string CurrentlyDeployedVersion { get; set; }
        public virtual string PackageName { get; set; }
        public virtual string Repository { get; set; }

        public virtual IList<TemplatedTaskParameterDto> Parameters { get; set; }
    }
}
