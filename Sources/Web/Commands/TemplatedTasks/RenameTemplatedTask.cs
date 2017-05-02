namespace Web.Commands
{
    public class RenameTemplatedTask : ICanBeRunOnlyByAdministrator
    {
        public int TemplatedTaskId { get; set; }

        public string Name { get; set; }
    }
}