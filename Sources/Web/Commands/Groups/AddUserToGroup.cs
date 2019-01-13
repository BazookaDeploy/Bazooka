using System;
namespace Web.Commands
{
    public class AddUserToGroup : ICommand, ICanBeRunByConfigurationManager
    {
        public string Group { get; set; }

        public Guid UserId { get; set; }
    }
}
