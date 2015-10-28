using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Web.Commands;
using Web.Models;

namespace Web.CommandHandlers
{
    public class AddUserToGroupHandler : CommandHandler<AddUserToGroup>
    {
        public override void Apply(AddUserToGroup command)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.AddToRole(command.UserId.ToString(), command.Group);
            context.SaveChanges();
        }
    }
}
