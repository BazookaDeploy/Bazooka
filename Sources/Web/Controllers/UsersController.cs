namespace Web.Controllers
{
    using DataAccess.Read;
    using DataAccess.Write;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Web.Commands;
    using Web.Models;

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

        //[HttpGet, Route("users/Allowed")]
        //public ICollection<AllowedUsersDto> AllowedUsers(int id)
        //{
        //    return db.AllowedUsers.Where(x => x.EnviromentId == id).OrderBy(x => x.UserName).ToList();
        //}

        //[HttpGet, Route("groups/Allowed")]
        //public ICollection<AllowedGroupsDto> AllowedGroups(int id)
        //{
        //    return db.AllowedGroups
        //             .Where(x => x.EnviromentId == id)
        //             .OrderBy(x => x.Name)
        //             .ToList();
        //}

        //[HttpPost, Route("users/allowed/add")]
        //public object AddAllowedUser(int enviromentId, string userId)
        //{
        //    if (db.AllowedUsers.Count(x => x.EnviromentId == enviromentId && x.USerId == userId) == 0)
        //    {
        //        using (var session = WebApiApplication.Store.OpenSession())
        //        {
        //            session.Save(new AllowedUser()
        //            {
        //                UserId = userId,
        //                EnviromentId = enviromentId
        //            });
        //            session.Flush();
        //        };
        //    }

        //    return new { Success = true };
        //}

        //[HttpPost, Route("users/allowed/remove")]
        //public object RemoveAllowedUser(int id)
        //{
        //    using (var session = WebApiApplication.Store.OpenSession())
        //    {

        //        session.Delete(session.Load<AllowedUser>(id));
        //        session.Flush();
        //    };
        //    return new { Success = true };
        //}

        //[HttpPost, Route("group/allowed/add")]
        //public object AddAllowedGroup(int enviromentId, string groupId)
        //{
        //    if (db.AllowedGroups.Count(x => x.EnviromentId == enviromentId && x.GroupId == groupId) == 0)
        //    {
        //        using (var session = WebApiApplication.Store.OpenSession())
        //        {
        //            session.Save(new AllowedGroup()
        //            {
        //                GroupId = groupId,
        //                EnviromentId = enviromentId
        //            });
        //            session.Flush();
        //        };
        //    }

        //    return new { Success = true };
        //}

        //[HttpPost, Route("group/allowed/remove")]
        //public object RemoveAllowedGroup(int id)
        //{
        //    using (var session = WebApiApplication.Store.OpenSession())
        //    {

        //        session.Delete(session.Load<AllowedGroup>(id));
        //        session.Flush();
        //    };

        //    return new { Success = true };
        //}
    }
}
