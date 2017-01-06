using DataAccess.Read;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Web.Commands;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Web.Controllers
{
    public class ApplicationsController : BaseController
    {
        public IReadContext db { get; set; }

        // GET: api/Applications
        public ICollection<ApplicationDto> Get()
        {
            var id = User.Identity.GetUserId();
            var user = db.Query<UserDto>().Single(x => x.Id == id);

            if (user.Administrator)
            {
                return db.Query<ApplicationDto>().OrderBy(x => x.Name).ToList();
            }
            else
            {
                var apps = db.Query<ApplicationAdministratorDto>().Where(x => x.UserId == id).Select(x => x.ApplicationId).ToList();
                return db.Query<ApplicationDto>().Where(x => apps.Contains(x.Id)).OrderBy(x => x.Name).ToList();
            }
        }

        [HttpGet]
        public ICollection<ApplicationDto> All()
        {
            return db.Query<ApplicationDto>().OrderBy(x => x.Name).ToList();
        }

        public ApplicationDto Get(int id)
        {
            return db.Query<ApplicationDto>().Single(x => x.Id == id);
        }

        [HttpPost]
        public ExecutionResult Create(CreateApplication command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult AddAllowedUser(AddAllowedUserToApplication command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult RemoveAllowedUser(RemoveAllowedUserFromApplication command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult AddAllowedGroup(AddAllowedGroupToApplication command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult RemoveAllowedGroup(RemoveAllowedGroupFromApplication command)
        {
            return Execute(command);
        }

        [HttpGet]
        public ICollection<AllowedUsersDto> AllowedUsers(int enviromentId, int applicationId)
        {
            return db.Query<AllowedUsersDto>().Where(x => x.EnviromentId == enviromentId && x.ApplicationId == applicationId).OrderBy(x => x.UserName).ToList();
        }

        [HttpGet]
        public ICollection<AllowedGroupsDto> AllowedGroups(int enviromentId, int applicationId)
        {
            return db.Query<AllowedGroupsDto>()
                     .Where(x => x.EnviromentId == enviromentId && x.ApplicationId == applicationId)
                     .OrderBy(x => x.Name)
                     .ToList();
        }

        [HttpGet]
        public ICollection<ApplicationAdministratorDto> Administrators(int applicationId)
        {
            return db.Query<ApplicationAdministratorDto>()
                     .Where(x => x.ApplicationId == applicationId)
                     .OrderBy(x => x.Name)
                     .ToList();
        }

        [HttpGet]
        public ICollection<ApplicationGroupDto> ApplicationGroups()
        {
            return db.Query<ApplicationGroupDto>().ToList();
        }

        [HttpPost]
        public ExecutionResult CreateApplicationGroup(CreateApplicationGroup command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult SetApplicationGroup(SetApplicationGroup command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult AddApplicationAdministrator(AddApplicationAdministrator command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult RemoveApplicationAdministrator(RemoveApplicationAdministrator command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult CopyConfigurationFromEnviroment(CopyConfigurationFromEnviroment command)
        {
            return Execute(command);
        }

        [HttpPost]
        public ExecutionResult CreateApplicationFromExisting(CreateApplicationFromExisting command)
        {
            return Execute(command);
        }
    }
}