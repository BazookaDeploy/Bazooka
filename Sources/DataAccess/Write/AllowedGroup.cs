using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Write
{
    public class AllowedGroup
    {

        public virtual int Id { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual string GroupId { get; set; }
    }
}
