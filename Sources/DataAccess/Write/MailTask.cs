namespace DataAccess.Write
{
    public class MailTask : IMovable
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Text { get; set; }

        public virtual string Recipients { get; set; }

        public virtual string Sender { get; set; }

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
