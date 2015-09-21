using System;
namespace Web.Commands
{
    public class AddUserToGroup : ICommand
    {
        public string Group { get; set; }

        public Guid UserId { get; set; }
    }
}
