using System;

namespace Web.Commands
{
    public abstract class ICommand
    {
        public int AggregateId { get; set; }

        public Guid CurrentUserId { get; set; }
    }
}
