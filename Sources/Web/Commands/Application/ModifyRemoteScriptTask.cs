namespace Web.Commands
{
    public class ModifyRemoteScriptTask : ICommand, ICanBeRunByApplicationAdministrator
    {
        public virtual int RemoteScriptTaskId { get; set; }

        public virtual string Script { get; set; }

        public virtual int AgentId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Folder { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual int ApplicationId { get; set; }
    }
}
