using System.Collections.Generic;
using System.Linq;
namespace DataAccess.Write
{
    public class Application
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<AllowedUser> AllowedUsers { get; set; }

        public virtual IList<AllowedGroup> AllowedGroups { get; set; }

        public virtual void AddAllowedUser(int enviromentId, System.Guid userId)
        {
            if (AllowedUsers.Count(x => x.EnviromentId == enviromentId && x.UserId == userId.ToString()) == 0)
            {
                AllowedUsers.Add(new AllowedUser()
                {
                    ApplicationId = this.Id,
                    EnviromentId = enviromentId,
                    UserId = userId.ToString()
                });
            }
        }

        public virtual void AddAllowedGroup(int enviromentId, string groupId)
        {
            if (AllowedGroups.Count(x => x.EnviromentId == enviromentId && x.GroupId == groupId) == 0)
            {
                AllowedGroups.Add(new AllowedGroup()
                {
                    ApplicationId = this.Id,
                    EnviromentId = enviromentId,
                    GroupId = groupId
                });
            }
        }
    }
}