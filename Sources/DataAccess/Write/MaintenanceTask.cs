using System;

namespace DataAccess.Write
{
    public class MaintenanceTask
    {
        public virtual int Id { get; set; }

        public virtual int AgentId { get; set; }

        public virtual int TemplatedTaskId { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual Status Status { get; set; }

        public virtual string UserId { get; set; }
    }
}