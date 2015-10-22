using System.Collections.Generic;
using System.Linq;
namespace DataAccess.Write
{
    public class Application
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int? ApplicationGroupId { get; set; }

        public virtual IList<AllowedUser> AllowedUsers { get; set; }

        public virtual IList<AllowedGroup> AllowedGroups { get; set; }

        public virtual IList<DatabaseTask> DatabaseTasks { get; set; }

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

        public virtual void RemoveAllowedUser(int enviromentId, System.Guid userId)
        {
            var user = this.AllowedUsers.SingleOrDefault(x => x.EnviromentId == enviromentId && x.UserId == userId.ToString());

            if (user != null)
            {
                this.AllowedUsers.Remove(user);
            }
        }

        public virtual void RemoveAllowedGroup(int enviromentId, string groupId)
        {
            var group = this.AllowedGroups.SingleOrDefault(x => x.EnviromentId == enviromentId && x.GroupId == groupId.ToString());

            if (group != null)
            {
                this.AllowedGroups.Remove(group);
            }
        }

        public virtual void SetApplicationGroup(int? applicationGroupId)
        {
            this.ApplicationGroupId = applicationGroupId;
        }

        public virtual void AddDatabaseTask(string name, string connectionString, string package, string dbName, int enviromentId, string repository, int agentId)
        {
            this.DatabaseTasks.Add(new DatabaseTask()
            {
                AgentId = agentId,
                ApplicationId = this.Id,
                ConnectionString = connectionString,
                DatabaseName = dbName,
                EnviromentId = enviromentId,
                Name = name,
                Package = package,
                Repository = repository
            });
        }

        public virtual void ModifyDatabaseTask(int taskId, string name, string connectionString, string package, string dbName, string repository, int agentId)
        {
            var task = this.DatabaseTasks.Single(x => x.Id == taskId);

            task.AgentId = agentId;
            task.ConnectionString = connectionString;
            task.DatabaseName = dbName;
            task.Name = name;
            task.Package = package;
            task.Repository = repository;
        }
    }
}