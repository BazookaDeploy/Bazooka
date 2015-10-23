namespace DataAccess.Write
{
    public class RemoteScriptTask
    {
        public virtual  int Id { get; set; }

        public virtual string Script { get; set; }

        public virtual int AgentId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Folder { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual int ApplicationId { get; set; }
    }
}
