namespace Web.Commands
{
    using Commands;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class CopyConfigurationFromEnviroment : ICommand
    {
        public int ApplicationId { get; set; }

        public int MachineId { get; set; }

        public int EnviromentId { get; set; }

        public int OriginalEnviromentId { get; set; }
    }
}