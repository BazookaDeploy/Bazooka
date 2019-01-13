namespace Web.Commands
{
    public class ChangeAgentAddress : ICommand, ICanBeRunByConfigurationManager
    {
        public int EnviromentId { get; set; }

        public int AgentId { get; set; }

        public string Address { get; set; }
    }
}
