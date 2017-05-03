namespace Web.Commands
{
    public class RenameTemplatedTask : ICommand, ICanBeRunOnlyByAdministrator
    {
        public int TemplatedTaskId { get; set; }

        public string Name { get; set; }
    }
}