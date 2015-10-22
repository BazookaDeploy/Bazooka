namespace Web.Commands
{
    public class ModifyDatabaseTask : ICommand
    {
        public virtual int DatabaseTaskId { get; set; }
        public virtual string Name { get; set; }
        public virtual string ConnectionString { get; set; }
        public virtual string Package { get; set; }
        public virtual string DatabaseName { get; set; }
        public virtual int ApplicationId { get; set; }
        public virtual string Repository { get; set; }
        public virtual int AgentId { get; set; }
    }
}
