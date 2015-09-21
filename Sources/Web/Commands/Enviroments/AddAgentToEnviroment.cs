namespace Web.Commands
{
    public class AddAgentToEnviroment : ICommand
    {
        public int EnviromentId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
