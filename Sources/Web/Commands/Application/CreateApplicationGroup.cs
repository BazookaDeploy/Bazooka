namespace Web.Commands
{
    public class CreateApplicationGroup : ICommand, ICanBeRunOnlyByAdministrator
    {
        public string Name { get; set; }
    }
}
