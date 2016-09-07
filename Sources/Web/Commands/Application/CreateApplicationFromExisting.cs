using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Commands
{
    public class CreateApplicationFromExisting : ICommand
    {
        public string Name { get; set; }

        public string OriginalApplicationId { get; set; }
    }
}