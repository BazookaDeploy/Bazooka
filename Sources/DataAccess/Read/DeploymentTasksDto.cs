namespace DataAccess.Read
{
    /// <summary>
    ///     Tasks assocaited with a deploy
    /// </summary>
    public class DeploymentTasksDto
    {
        public int Id { get; set; }

        public int DeployTaskId { get; set; }

        public int DeploymentId { get; set; }

        public string Name { get; set; }

        public TaskType DeployType { get; set; }
    }
}
