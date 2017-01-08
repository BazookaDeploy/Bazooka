namespace Web.Commands
{
    public class AddApplicationAdministrator : ICommand, ICanBeRunByApplicationAdministrator
    {
            public int ApplicationId { get; set; }

            public System.Guid UserId { get; set; }
        }
    }
