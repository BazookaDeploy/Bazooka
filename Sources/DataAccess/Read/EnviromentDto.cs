using System.Collections;
using System.Collections.Generic;

namespace DataAccess.Read
{
    public class EnviromentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<AgentDto> Agents { get; set; }
    }
}