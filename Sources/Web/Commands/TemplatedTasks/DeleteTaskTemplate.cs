namespace Web.Commands
{
    public class DeleteTaskTemplate : ICommand, ICanBeRunOnlyByAdministrator
    {
        public int TemplatedTaskId { get; set; }
    }
}