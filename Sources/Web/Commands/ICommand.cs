using System;

namespace Web.Commands
{
    public class ICommand
    {
        public int AggregateId { get; set; }

        public Guid CurrentUserId { get; set; }
    }
}
