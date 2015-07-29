using System;
namespace DataAccess.Write
{
    public class LocalScriptTask
    {
        public virtual  int Id { get; set; }

        public virtual string Script { get; set; }

        public virtual string Name { get; set; }

        public virtual int EnviromentId { get; set; }
    }
}
