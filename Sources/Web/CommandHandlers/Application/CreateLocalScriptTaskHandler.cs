using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateLocalScriptTaskHandler : CommandHandler<CreateLocalScriptTask>
    {
        public override void Apply(CreateLocalScriptTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.CreateLocalScriptTask(command.EnviromentId, command.Script, command.Name);
            Repository.Save<Application>(application);
        }
    }
}
