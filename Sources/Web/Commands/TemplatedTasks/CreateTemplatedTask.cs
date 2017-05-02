namespace Web.Commands
{
    public class CreateTemplatedTask : ICanBeRunOnlyByAdministrator
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}