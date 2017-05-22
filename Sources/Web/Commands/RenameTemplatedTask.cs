using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Commands
{
    public class RenameTemplatedTask : ICommand, ICanBeRunByApplicationAdministrator
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int EnviromentId { get; set; }
        public virtual int ApplicationId { get; set; }
    }
}