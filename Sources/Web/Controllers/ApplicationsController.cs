using DataAccess.Read;
using DataAccess.Write;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Web.Commands;

namespace Web.Controllers
{
    public class ApplicationsController : BaseController
    {
        public IReadContext db { get; set; }

        // GET: api/Applications
        public ICollection<ApplicationDto> Get()
        {
            return db.Query<ApplicationDto>().ToList();
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
        public ExecutionResult AddAllowedGroup(AddAllowedGroupToApplication command)
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

    }
}