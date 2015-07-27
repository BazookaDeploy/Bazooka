using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Write
{
    public class MailTask
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Text { get; set; }

        public virtual string Recipients { get; set; }

        public virtual string Sender { get; set; }

        public virtual int EnviromentId { get; set; }
    }
}
