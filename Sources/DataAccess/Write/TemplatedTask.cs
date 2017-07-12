using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Write
{
    public class TemplatedTask : IMovable
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int EnviromentId { get; set; }
        public virtual int ApplicationId { get; set; }
        public virtual int AgentId { get; set; }
        public virtual int TaskTemplateVersionId { get; set; }

        public virtual string CurrentlyDeployedVersion  { get; set; }
        public virtual string PackageName { get; set; }
        public virtual string Repository { get; set; }

        public virtual IList<TemplatedTaskParameter> Prameters { get; set; }

        public virtual int Position { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual void MoveUp()
        {
            this.Position--;
        }

        public virtual void MoveDown()
        {
            this.Position++;
        }
    }
}
