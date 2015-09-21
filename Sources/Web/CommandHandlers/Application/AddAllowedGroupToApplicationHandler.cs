using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class AddAllowedGroupToApplicationHandler : CommandHandler<AddAllowedGroupToApplication>
    {
        public override void Apply(AddAllowedGroupToApplication command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.AddAllowedGroup(command.EnviromentId, command.GroupId);
            Repository.Save<Application>(application);
        }
    }
}
