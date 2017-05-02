namespace Web.Commands
{
    public class ChangeTemplatedTaskDescription : ICanBeRunOnlyByAdministrator
    {
        public int TemplatedTaskId { get; set; }

        public string Description { get; set; }
    }
}