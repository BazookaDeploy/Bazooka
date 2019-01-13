namespace Web.Commands
{
    public class CreateApplication : ICommand, ICanBeRunByConfigurationManager
    {
        public string Name { get; set; }
    }
}
