namespace Web.Commands
{
    public class AddApplicationAdministrator : ICommand
        {
            public int ApplicationId { get; set; }

            public System.Guid UserId { get; set; }
        }
    }
