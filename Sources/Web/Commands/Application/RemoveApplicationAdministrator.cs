namespace Web.Commands
{
    public class RemoveApplicationAdministrator : ICommand, ICanBeRunByApplicationAdministrator
    {
        public int ApplicationId { get; set; }

        public System.Guid UserId { get; set; }
    }
}
