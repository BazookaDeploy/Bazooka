using System;
namespace Web.Commands
{
    public class RemoveUserFromGroup : ICommand, ICanBeRunByConfigurationManager
    {
        public string Group { get; set; }

        public Guid UserId { get; set; }
    }
}
