using System.Collections.Generic;

namespace DataAccess.Write
{
    public class Application
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Enviroment> Enviroments { get; set; }
    }
}