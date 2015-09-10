using DataAccess.Write;
using System;
using System.Collections.Generic;

namespace DataAccess.Read
{
    public class DeploymentDto
    {
        public virtual int Id { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual int ApplicationId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Enviroment { get; set; }

        public virtual string Configuration { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual Status Status { get; set; }

        public virtual string Version { get; set; }

        public virtual string UserId { get; set; }

        public virtual string UserName { get; set; }

        public virtual ICollection<LogEntryDto> Logs { get; set; }
    }

    public class LogEntryDto
    {
        public virtual int Id { get; set; }

        public virtual DateTime TimeStamp { get; set; }

        public virtual string Text { get; set; }

        public virtual bool Error { get; set; }

        public virtual int DeploymentId { get; set; }
    }
}
