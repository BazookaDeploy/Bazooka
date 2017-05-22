namespace Web.Commands
{
    public class ChangeTaskTemplateDescription : ICommand, ICanBeRunOnlyByAdministrator
    {
        public int TemplatedTaskId { get; set; }

        public string Description { get; set; }
    }
}