namespace Web.Commands
{
    public class ModifyLocalScriptTask : ICommand
    {
        public virtual int LocalScriptTaskId { get; set; }

        public virtual string Script { get; set; }

        public virtual string Name { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual int ApplicationId { get; set; }
    }
}
