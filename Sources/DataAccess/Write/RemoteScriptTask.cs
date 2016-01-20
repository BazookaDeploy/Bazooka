namespace DataAccess.Write
{
    public class RemoteScriptTask : IMovable
    {
        public virtual  int Id { get; set; }

        public virtual string Script { get; set; }

        public virtual int AgentId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Folder { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual int ApplicationId { get; set; }

        public virtual int Position { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual void MoveUp()
        {
            this.Position--;
        }

        public virtual void MoveDown()
        {
            this.Position++;
        }
    }
}
