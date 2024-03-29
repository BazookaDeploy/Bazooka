﻿namespace DataAccess.Write
{
    public class DatabaseTask : IMovable
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ConnectionString { get; set; }
        public virtual string Package { get; set; }
        public virtual string DatabaseName { get; set; }
        public virtual int EnviromentId { get; set; }
        public virtual int ApplicationId { get; set; }
        public virtual string Repository { get; set; }
        public virtual int AgentId { get; set; }
        public virtual int Position { get; set; }

        public virtual bool Partial { get; set; }

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
