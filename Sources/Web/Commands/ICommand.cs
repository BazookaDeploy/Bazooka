using System;

namespace Web.Commands
{
    public class ICommand
    {
        public int Aggregateid { get; set; }

        public Guid CurrentUserId { get; set; }
    }
}
