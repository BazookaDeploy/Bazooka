namespace Web.Commands.Application
{
    public class RenameApplication : ICommand, ICanBeRunByApplicationAdministrator
    {
        public int ApplicationId { get; set; }

        public string Name { get; set; }
    }
}