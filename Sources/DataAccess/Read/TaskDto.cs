namespace DataAccess.Read
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TaskType Type { get; set; }

        public int EnviromentId { get; set; }

        public int ApplicationId { get; set; }
    }
}
