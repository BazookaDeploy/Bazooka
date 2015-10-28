using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class ModifyMailTaskHandler : CommandHandler<ModifyMailTask>
    {
        public override void Apply(ModifyMailTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.ModifyMailTask(command.MailTaskId, command.EnviromentId, command.Name, command.Text, command.Recipients, command.Sender);
            Repository.Save<Application>(application);
        }
    }
}
