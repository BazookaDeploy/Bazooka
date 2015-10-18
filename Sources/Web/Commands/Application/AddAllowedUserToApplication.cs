using System;

namespace Web.Commands
{
    public class AddAllowedUserToApplication : ICommand
    {
        public int ApplicationId { get; set; }

        public int EnviromentId { get; set; }

        public Guid UserId { get; set; }
    }
}
