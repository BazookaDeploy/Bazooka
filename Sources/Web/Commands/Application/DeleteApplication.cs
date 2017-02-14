namespace Web.Commands
{
    public class DeleteApplication : ICommand, ICanBeRunByApplicationAdministrator
    {
        public int ApplicationId { get; set; }
    }
}