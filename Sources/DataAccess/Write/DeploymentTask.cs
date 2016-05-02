namespace DataAccess.Write
{
    public class DeploymentTask
    {
        public virtual int Id { get; set; }

        public virtual int DeploymentId { get; set; }

        public virtual int DeployTaskId { get; set; }

        public virtual int DeployType { get; set; }
    }
}