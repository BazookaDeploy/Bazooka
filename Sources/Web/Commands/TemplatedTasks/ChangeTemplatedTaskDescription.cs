namespace Web.Commands
{
    public class ChangeTemplatedTaskDescription : ICommand, ICanBeRunOnlyByAdministrator
    {
        public int TemplatedTaskId { get; set; }

        public string Description { get; set; }
    }
}