namespace Web.Commands
{
    public class CreateMailTask : ICommand
    {
        public virtual string Name { get; set; }

        public virtual string Text { get; set; }

        public virtual string Recipients { get; set; }

        public virtual string Sender { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual int ApplicationId { get; set; }
    }
}
