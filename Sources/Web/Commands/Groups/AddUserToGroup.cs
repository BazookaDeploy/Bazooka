using System;
namespace Web.Commands
{
    public class AddUserToGroup : ICommand, ICanBeRunOnlyByAdministrator
    {
        public string Group { get; set; }

        public Guid UserId { get; set; }
    }
}
