using DataAccess.Write;
using System;
using System.Collections.Generic;

namespace DataAccess.Read
{
    public class MaintenanceTaskDto
    {

        public virtual int Id { get; set; }

        public virtual int AgentId { get; set; }

        public string Agent { get; set; }

        public virtual int TemplatedTaskId { get; set; }

        public string TaskName { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual Status Status { get; set; }

        public virtual string UserId { get; set; }

        public string UserName { get; set; }

        public virtual ICollection<MaintenanceLogEntryDto> Logs { get; set; }
    }
}
