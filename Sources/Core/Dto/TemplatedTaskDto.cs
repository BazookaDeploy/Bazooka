using System.Collections.Generic;

namespace Bazooka.Core.Dto
{
    public class TemplatedTaskDto
    {
        public string Script { get; set; }

        public ICollection<ParameterDto> Parameters { get; set; }
    }

    public class ParameterDto
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
