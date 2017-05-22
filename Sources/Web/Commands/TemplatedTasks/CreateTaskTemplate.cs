namespace Web.Commands
{
    public class CreateTaskTemplate : ICommand, ICanBeRunOnlyByAdministrator
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}