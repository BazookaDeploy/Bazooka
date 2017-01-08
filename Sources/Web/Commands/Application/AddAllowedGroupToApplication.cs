namespace Web.Commands
{
    public class AddAllowedGroupToApplication : ICommand, ICanBeRunByApplicationAdministrator
    {
        public int ApplicationId { get; set; }

        public int EnviromentId { get; set; }

        public string GroupId { get; set; }
    }
}
