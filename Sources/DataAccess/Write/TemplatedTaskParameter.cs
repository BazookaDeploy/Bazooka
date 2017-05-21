using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Write
{
    public class TemplatedTaskParameter
    {
        public virtual int Id { get; set; }

        public virtual TemplatedTask TemplatedTask { get; set; }

        public virtual int TaskTemplateParameterId { get; set; }

        public virtual string Value { get; set; }
    }
}
