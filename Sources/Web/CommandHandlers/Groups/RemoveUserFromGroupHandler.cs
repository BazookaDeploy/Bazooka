using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Web.Commands;
using Web.Models;

namespace Web.CommandHandlers
{
    public class RemoveUserFromGroupHandler : CommandHandler<RemoveUserFromGroup>
    {
        public override void Apply(RemoveUserFromGroup command)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.RemoveFromRole(command.UserId.ToString(), command.Group);
            context.SaveChanges();
        }
    }
}
