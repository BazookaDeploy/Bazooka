using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateEnviromentHandler : CommandHandler<CreateEnviroment>
    {
        public override void Apply(CreateEnviroment command)
        {
            Repository.Save(new Enviroment() { Name = command.Name });
        }
    }
}
