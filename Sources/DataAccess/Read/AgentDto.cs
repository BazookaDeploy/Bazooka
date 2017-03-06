using System;

namespace DataAccess.Read
{
    public class AgentDto
    {
        public int Id { get; set; }

        public int EnviromentId { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        public DateTime? LastStatusCheck { get; set; }

        public bool LastCheck { get; set; }
    }
}
