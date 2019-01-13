namespace Web.Commands
{
    public class RenameEnviromentAgent : ICommand, ICanBeRunByConfigurationManager
    {
        public int EnviromentId { get; set; }

        public int AgentId { get; set; }

        public string Name { get; set; }
    }
}
