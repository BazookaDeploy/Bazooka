namespace Web.Commands
{
    public class CreateGroup : ICommand, ICanBeRunOnlyByAdministrator
    {
        public string Name { get; set; }
    }
}
