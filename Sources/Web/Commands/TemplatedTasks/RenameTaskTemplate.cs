namespace Web.Commands
{
    public class RenameTaskTemplate : ICommand, ICanBeRunOnlyByAdministrator
    {
        public int TemplatedTaskId { get; set; }

        public string Name { get; set; }
    }
}