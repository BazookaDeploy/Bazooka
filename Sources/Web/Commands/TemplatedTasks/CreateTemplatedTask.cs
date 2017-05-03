namespace Web.Commands
{
    public class CreateTemplatedTask : ICommand, ICanBeRunOnlyByAdministrator
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}