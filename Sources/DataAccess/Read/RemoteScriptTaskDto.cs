namespace DataAccess.Read
{
    public class RemoteScriptTaskDto
    {
        public int Id { get; set; }

        public string Script { get; set; }

        public string Machine { get; set; }

        public string Name { get; set; }

        public int EnviromentId { get; set; }
    }
}
