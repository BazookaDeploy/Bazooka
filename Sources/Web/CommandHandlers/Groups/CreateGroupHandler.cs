using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Web.Commands;
using Web.Models;
namespace Web.CommandHandlers
{
    public class CreateGroupHandler : CommandHandler<CreateGroup>
    {
        public override void Apply(CreateGroup command)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!roleManager.RoleExists(command.Name))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = command.Name;
                roleManager.Create(role);
            }
        }
    }
}
