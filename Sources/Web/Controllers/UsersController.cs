namespace Web.Controllers
{
    using DataAccess.Read;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Web.Commands;

    public class UsersController : BaseController
    {
        public IReadContext db {get; set;}

        [HttpGet]
        public ICollection<UserDto> All()
        {
            return db.Query<UserDto>().ToList();
        }


        [HttpGet]
        public ICollection<GroupDto> Groups()
        {
            return db.Query<GroupDto>()
                     .OrderBy(x => x.Name)
                     .ToList();
        }

        [HttpGet]
        public ICollection<UsersInGroupsDto> Group(string groupName)
        {
            return db.Query<UsersInGroupsDto>()
                     .Where(x => x.RoleName == groupName)
                     .OrderBy(x => x.UserName)
                     .ToList();
        }

        [HttpPost]
        public ExecutionResult CreateGroup(CreateGroup command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult AddUserToGroup(AddUserToGroup command)
        {
            return Execute(command);
        }

        [HttpPost]
        public object RemoveUserFromGroup(RemoveUserFromGroup command)
        {
            return Execute(command);
        }
    }
}
