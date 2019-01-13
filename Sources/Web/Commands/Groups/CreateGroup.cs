namespace Web.Commands
{
    public class CreateGroup : ICommand, ICanBeRunByConfigurationManager
    {
        public string Name { get; set; }
    }
}
