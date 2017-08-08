using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Commands.Application
{
    public class MoveTaskDown : ICommand, ICanBeRunByApplicationAdministrator
    {
        public virtual int EnviromentId { get; set; }
        public virtual int ApplicationId { get; set; }
        public virtual int Position { get; set; }
    }

}