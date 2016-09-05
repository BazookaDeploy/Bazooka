namespace Web.CommandHandlers
{
    using Commands;
    using DataAccess.Write;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class CopyConfigurationFromEnviromentHandler : CommandHandler<CopyConfigurationFromEnviroment>
    {
        public override void Apply(CopyConfigurationFromEnviroment command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.CopyConfigurationFromEnviroment(command.EnviromentId, command.OriginalEnviromentId, command.MachineId);
            Repository.Save<Application>(application);
        }
    }
}