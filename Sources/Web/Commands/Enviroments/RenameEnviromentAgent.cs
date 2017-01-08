namespace Web.Commands
{
    public class RenameEnviromentAgent : ICommand, ICanBeRunOnlyByAdministrator
    {
        public int EnviromentId { get; set; }

        public int AgentId { get; set; }

        public string Name { get; set; }
    }
}
