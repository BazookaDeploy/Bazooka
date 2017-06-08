using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Commands
{
    public class UpdateTemplatedTask : ICommand, ICanBeRunByApplicationAdministrator
    {
        public virtual int Id { get; set; }
        public virtual int ApplicationId { get; set; }
        public virtual int Version { get; set; }
    }

}