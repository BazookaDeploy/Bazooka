namespace Web.Commands
{
    public class CreateApplication : ICommand, ICanBeRunOnlyByAdministrator
    {
        public string Name { get; set; }
    }
}
