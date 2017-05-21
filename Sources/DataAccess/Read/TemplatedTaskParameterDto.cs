using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Read
{
    public class TemplatedTaskParameterDto
    {
        public virtual int Id { get; set; }

        public virtual int TaskTemplateParameterId { get; set; }

        public virtual string Value { get; set; }
    }
}
