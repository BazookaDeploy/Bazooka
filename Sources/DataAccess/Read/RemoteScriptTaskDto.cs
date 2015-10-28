namespace DataAccess.Read
{
    public class RemoteScriptTaskDto
    {
        public int Id { get; set; }

        public string Script { get; set; }

        public int AgentId { get; set; }

        public string AgentName { get; set; }

        public string Address { get; set; }

        public string Name { get; set; }

        public string Folder { get; set; }

        public int EnviromentId { get; set; }

        public int ApplicationId { get; set; }
    }
}
