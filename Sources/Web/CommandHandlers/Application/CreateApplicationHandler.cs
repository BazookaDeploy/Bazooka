using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateApplicationHandler : CommandHandler<CreateApplication>
    {
        public override void Apply(CreateApplication command)
        {
            Repository.Save(new Application()
            {
                Name = command.Name,                
            });
        }
    }
}
