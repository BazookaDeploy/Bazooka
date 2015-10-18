using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateApplicationGroupHandler : CommandHandler<CreateApplicationGroup>
    {
        public override void Apply(CreateApplicationGroup command)
        {
            Repository.Save<ApplicationGroup>(new ApplicationGroup()
            {
                Name = command.Name
            });
        }
    }
}
