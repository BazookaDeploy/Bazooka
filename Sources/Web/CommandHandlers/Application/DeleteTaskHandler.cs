namespace Web.CommandHandlers
{
    using Commands;
    using Commands.Application;
    using DataAccess.Write;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DeleteTaskHandler : CommandHandler<DeleteTask>
    {
        public override void Apply(DeleteTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.DeleteTask(command.TaskId, command.Type);
            Repository.Save<Application>(application);
        }
    }
}
