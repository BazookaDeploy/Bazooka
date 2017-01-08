using System;
namespace Web.Commands
{
    public class RemoveUserFromGroup : ICommand, ICanBeRunOnlyByAdministrator
    {
        public string Group { get; set; }

        public Guid UserId { get; set; }
    }
}
