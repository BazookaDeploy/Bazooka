using System.Collections.Generic;

namespace DataAccess.Read
{
    public class EnviromentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AgentDto> Agents { get; set; }
    }
}