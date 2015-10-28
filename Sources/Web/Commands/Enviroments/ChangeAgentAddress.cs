namespace Web.Commands
{
    public class ChangeAgentAddress : ICommand
    {
        public int EnviromentId { get; set; }

        public int AgentId { get; set; }

        public string Address { get; set; }
    }
}
