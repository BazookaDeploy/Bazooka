using System;
namespace Web.Commands
{
    public class RemoveUserFromGroup : ICommand
    {
        public string Group { get; set; }

        public Guid UserId { get; set; }
    }
}
