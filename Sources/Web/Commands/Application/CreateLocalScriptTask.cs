namespace Web.Commands
{
    public class CreateLocalScriptTask : ICommand, ICanBeRunByApplicationAdministrator
    {
        public virtual string Script { get; set; }

        public virtual string Name { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual int ApplicationId { get; set; }
    }
}
