using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateMailTaskHandler : CommandHandler<CreateMailTask>
    {
        public override void Apply(CreateMailTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.CreateMailTask(command.EnviromentId,command.Name,command.Text,command.Recipients,command.Sender);
            Repository.Save<Application>(application);
        }
    }
}
