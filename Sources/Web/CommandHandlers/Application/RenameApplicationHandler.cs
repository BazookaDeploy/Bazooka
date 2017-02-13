using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Commands;
using Web.Commands.Application;

namespace Web.CommandHandlers
{
    public class RenameApplicationHandler : CommandHandler<RenameApplication>
    {
        public override void Apply(RenameApplication command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.Rename(command.Name);
            Repository.Save<Application>(application);
        }
    }
}