using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Commands
{
    public class CreateApplicationFromExisting : ICommand,ICanBeRunOnlyByAdministrator
    {
        public string Name { get; set; }

        public int OriginalApplicationId { get; set; }
    }
}