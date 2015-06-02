namespace Web.Controllers
{
    using DataAccess.Read;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Web.Models;

    public class UsersController : ApiController
    {
        private ReadContext db = new ReadContext();

        [HttpGet, Route("users/all")]
        public ICollection<UserDto> Users()
        {
            return db.Users.ToList();
        }


        [HttpGet, Route("users/groups")]
        public ICollection<GroupDto> Groups()
        {
            return db.Groups.ToList();
        }

        [HttpGet, Route("users/group")]
        public ICollection<UsersInGroupsDto> UsersInGroup(string groupName)
        {
            return db.UsersInGroups.Where(x => x.RoleName ==groupName).ToList();
        }

        [HttpPost, Route("users/group")]
        public object CreateGroup(string groupName)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));


            if (!roleManager.RoleExists(groupName))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = groupName;
                roleManager.Create(role);

            }

            return new { Success = true };
        }

        [HttpPost, Route("users/add")]
        public object AddUserToGroup(string group, string userId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            try
            {
                UserManager.AddToRole(userId, group);
                context.SaveChanges();
            }
            catch
            {
                return new { Success = false };
            }
            return new { Success = true };
        }


        [HttpPost, Route("users/remove")]
        public object RemoveUserFromGroup(string group, string userId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            try
            {
                UserManager.RemoveFromRole(userId, group);
                context.SaveChanges();
            }
            catch
            {
                return new { Success = false };
            }
            return new { Success = true };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
