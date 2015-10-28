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

        public virtual IList<DeployTask> DeployTasks { get; set; }

        public virtual IList<MailTask> MailTasks { get; set; }

        public virtual IList<LocalScriptTask> LocalScriptTasks { get; set; }

        public virtual IList<RemoteScriptTask> RemoteScriptTasks { get; set; }

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

        public virtual void AddDeployTask(int enviromentId, string name, int agentID, string package, string directory, string repository, string configuration, IList<Parameter> parameters)
        {
            this.DeployTasks.Add(new DeployTask()
            {
                AgentId = agentID,
                ApplicationId = this.Id,
                Configuration = configuration,
                Directory = directory,
                EnviromentId = enviromentId,
                Name = name,
                PackageName = package,
                Repository = repository,
                AdditionalParameters = parameters
            });
        }

        public virtual void ModifyDeployTask(int id, string name, int agentId, string package, string directory, string repo, string installScript, string uninstallScript, string configFile, string transform, string configuration, IList<Parameter> parameters)
        {
            var task = this.DeployTasks.Single(x => x.Id == id);

            task.AgentId = agentId;
            task.Configuration = configuration;
            task.ConfigurationFile = configFile;
            task.ConfigurationTransform = transform;
            task.Directory = directory;
            task.InstallScript = installScript;
            task.Name = name;
            task.PackageName = package;
            task.Repository = repo;
            task.UninstallScript = uninstallScript;

            foreach (var param in parameters)
            {
                if (param.ParameterId != 0)
                {
                    var par = task.AdditionalParameters.Single(x => x.ParameterId == param.ParameterId);
                    par.Encrypted = param.Encrypted;
                    par.Key = param.Key;
                    par.Value = param.Value;
                }
                else
                {
                    task.AdditionalParameters.Add(new Parameter() { DeployTaskId = task.Id, Encrypted = param.Encrypted, Key = param.Key, Value = param.Value });
                }
            }

            var list = new List<Parameter>();
            foreach (var param in task.AdditionalParameters)
            {
                if (param.ParameterId != 0)
                {
                    if (parameters.Count(x => x.ParameterId == param.ParameterId) == 0)
                    {
                        list.Add(param);
                    }
                }
            }

            foreach (var param in list)
            {
                task.AdditionalParameters.Remove(param);
            }
        }

        public virtual void CreateLocalScriptTask(int enviromentId, string script, string name)
        {
            this.LocalScriptTasks.Add(new LocalScriptTask()
            {
                ApplicationId = this.Id,
                EnviromentId = enviromentId,
                Name = name,
                Script = script
            });
        }

        public virtual void ModifyLocalScriptTask(int id, int enviromentId, string script, string name)
        {
            var task = this.LocalScriptTasks.Single(x => x.Id == id);
            task.Name = name;
            task.Script = script;
        }

        public virtual void CreateMailTask(int enviromentId, string name, string text, string recipients, string sender)
        {
            this.MailTasks.Add(new MailTask()
            {
                ApplicationId = this.Id,
                EnviromentId = enviromentId,
                Name = name,
                Recipients = recipients,
                Sender = sender,
                Text = text
            });
        }

        public virtual void ModifyMailTask(int id, int enviromentId, string name, string text, string recipients, string sender)
        {
            var task = this.MailTasks.Single(x => x.Id == id);
            task.Name = name;
            task.Recipients = recipients;
            task.Sender = sender;
            task.Text = text;
        }

        public virtual void CreateRemoteScriptTask(int enviromentId, string script, string name, string folder, int agentId)
        {
            this.RemoteScriptTasks.Add(new RemoteScriptTask()
            {
                ApplicationId = this.Id,
                EnviromentId = enviromentId,
                Name = name,
                Script = script,
                AgentId = agentId,
                Folder = folder
            });
        }

        public virtual void ModifyRemoteScriptTask(int id, int enviromentId, string script, string name, string folder, int agentId)
        {
            var task = this.RemoteScriptTasks.Single(x => x.Id == id);
            task.Name = name;
            task.Script = script;
            task.AgentId = agentId;
            task.Folder = folder;
        }
    }
}