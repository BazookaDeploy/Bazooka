﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Read;

namespace DataAccess.Write
{
    public class Application
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual Guid Secret { get; set; }

        public virtual int? ApplicationGroupId { get; set; }

        public virtual IList<AllowedUser> AllowedUsers { get; set; }

        public virtual IList<AllowedGroup> AllowedGroups { get; set; }

        public virtual IList<DatabaseTask> DatabaseTasks { get; set; }

        public virtual IList<DeployTask> DeployTasks { get; set; }

        public virtual IList<MailTask> MailTasks { get; set; }

        public virtual IList<LocalScriptTask> LocalScriptTasks { get; set; }

        public virtual IList<RemoteScriptTask> RemoteScriptTasks { get; set; }

        public virtual IList<TemplatedTask> TemplatedTasks { get; set; }

        public virtual IList<ApplicationAdministrator> Administrators { get; set; }

        public virtual IList<IMovable> AllTasks(int enviromentId)
        {
            var list = new List<IMovable>();

            if (DatabaseTasks != null)
            {
                list.AddRange(DatabaseTasks.Where(x => x.EnviromentId == enviromentId && !x.Deleted).Select(x => (IMovable)x).ToList());
            }

            if (DeployTasks != null)
            {
                list.AddRange(DeployTasks.Where(x => x.EnviromentId == enviromentId && !x.Deleted).Select(x => (IMovable)x).ToList());
            }

            if (MailTasks != null)
            {
                list.AddRange(MailTasks.Where(x => x.EnviromentId == enviromentId && !x.Deleted).Select(x => (IMovable)x).ToList());
            }

            if (LocalScriptTasks != null)
            {
                list.AddRange(LocalScriptTasks.Where(x => x.EnviromentId == enviromentId && !x.Deleted).Select(x => (IMovable)x).ToList());
            }

            if (RemoteScriptTasks != null)
            {
                list.AddRange(RemoteScriptTasks.Where(x => x.EnviromentId == enviromentId && !x.Deleted).Select(x => (IMovable)x).ToList());
            }

            if (TemplatedTasks != null)
            {
                list.AddRange(TemplatedTasks.Where(x => x.EnviromentId == enviromentId && !x.Deleted).Select(x => (IMovable)x).ToList());
            }

            return list.OrderBy(x => x.Position).ToList();
        }

        public virtual void MoveUp(int position, int enviromentId)
        {
            var tasks = this.AllTasks(enviromentId);
            var toMove = tasks.Single(x => x.Position == position);
            var before = tasks.LastOrDefault(x => x.Position < toMove.Position);

            if (before != null)
            {
                var tPos = toMove.Position;
                toMove.Position = before.Position;
                before.Position = tPos;
            }
        }

        public virtual void MoveDown(int position, int enviromentId)
        {
            var tasks = this.AllTasks(enviromentId);
            var toMove = tasks.Single(x => x.Position == position);
            var before = tasks.FirstOrDefault(x => x.Position > toMove.Position);

            if (before != null)
            {
                var tPos = toMove.Position;
                toMove.Position = before.Position;
                before.Position = tPos;
            }
        }

        public virtual void ModifyTemplatedTask(int id, int agentId, int enviromentId, string packageName, string repository,  IEnumerable<TemplatedTaskParameter> enumerable)
        {
            var task = this.TemplatedTasks.Single(x => x.Id == id);

            task.AgentId = agentId;
            task.PackageName = packageName;
            task.Repository = repository;

            enumerable.ToList().ForEach(x =>
            {
                task.Prameters.Single(z => z.TaskTemplateParameterId == x.TaskTemplateParameterId).Value = x.Value;
            });
        }

        public virtual void UpdateTemplatedTask(int id, int version, ICollection<Tuple<int,int>> newVersions, ICollection<int> newParams)
        {
            var task = this.TemplatedTasks.Single(x => x.Id == id);

            task.TaskTemplateVersionId = version;
            foreach(var par in newVersions)
            {
                foreach(var param in task.Prameters)
                {
                    if(par.Item1 == param.TaskTemplateParameterId)
                    {
                        param.TaskTemplateParameterId = par.Item2;
                    }
                }
            }

            foreach (var par in newParams)
            {
                task.Prameters.Add(new TemplatedTaskParameter()
                {
                    TaskTemplateParameterId = par,
                    TemplatedTask = task,
                    Value = ""
                });
            }
        }

        public virtual void RenameTemplatedTask(int id, int enviromentId, string name)
        {
            var task = this.TemplatedTasks.Single(x => x.Id == id && x.EnviromentId == enviromentId);

            task.Name = name;
        }

        public virtual void AddTemplatedTask(int agentId, int enviromentId, int version, string name, IEnumerable<TemplatedTaskParameter> enumerable)
        {
            var a = new TemplatedTask()
            {
                AgentId = agentId,
                ApplicationId = this.Id,
                EnviromentId = enviromentId,
                Name = name,
                TaskTemplateVersionId = version,
                Deleted = false,
                Position = this.NewTaskNumber(enviromentId)
            };

            a.Prameters = enumerable.Select(x => new TemplatedTaskParameter()
            {
                TemplatedTask = a,
                TaskTemplateParameterId = x.TaskTemplateParameterId,
                Value = x.Value
            }).ToList();

            this.TemplatedTasks.Add(a);
        }

        public virtual void AddAdministrator(System.Guid userId)
        {
            if (Administrators.Count(x => x.UserId == userId.ToString()) == 0)
            {
                Administrators.Add(new ApplicationAdministrator()
                {
                    ApplicationId = this.Id,
                    UserId = userId.ToString()
                });
            }
        }

        public virtual void RemoveAdministrator(System.Guid userId)
        {
            var user = this.Administrators.SingleOrDefault(x => x.UserId == userId.ToString());

            if (user != null)
            {
                this.Administrators.Remove(user);
            }
        }

        public virtual void CopyConfigurationFromEnviroment(int enviromentId, int originalEnviromentId, int machineId)
        {
            if (DatabaseTasks != null)
            {
                var tasks = this.DatabaseTasks.Where(x => x.EnviromentId == originalEnviromentId && !x.Deleted).Select(x => new DatabaseTask()
                {
                    AgentId = machineId,
                    ApplicationId = x.ApplicationId,
                    ConnectionString = x.ConnectionString,
                    DatabaseName = x.DatabaseName,
                    EnviromentId = enviromentId,
                    Name = x.Name,
                    Package = x.Package,
                    Repository = x.Repository,
                    Position = this.NewTaskNumber(enviromentId)
                }).ToList();

                foreach (var task in tasks) { this.DatabaseTasks.Add(task); }
            }

            if (DeployTasks != null)
            {
                var tasks = this.DeployTasks.Where(x => x.EnviromentId == originalEnviromentId && !x.Deleted).Select(x => new DeployTask()
                {
                    AgentId = machineId,
                    ApplicationId = x.ApplicationId,
                    Configuration = x.Configuration,
                    ConfigurationFile = x.ConfigurationFile,
                    ConfigurationTransform = x.ConfigurationTransform,
                    Directory = x.Directory,
                    EnviromentId = enviromentId,
                    InstallScript = x.InstallScript,
                    Name = x.Name,
                    PackageName = x.PackageName,
                    Repository = x.Repository,
                    UninstallScript = x.UninstallScript,
                    Position = this.NewTaskNumber(enviromentId)
                }).ToList();

                foreach (var task in tasks) { this.DeployTasks.Add(task); }
            }

            if (MailTasks != null)
            {
                var tasks = this.MailTasks.Where(x => x.EnviromentId == originalEnviromentId && !x.Deleted).Select(x => new MailTask()
                {
                    ApplicationId = x.ApplicationId,
                    EnviromentId = enviromentId,
                    Name = x.Name,
                    Position = this.NewTaskNumber(enviromentId),
                    Recipients = x.Recipients,
                    Sender = x.Sender,
                    Text = x.Text
                }).ToList();

                foreach (var task in tasks) { this.MailTasks.Add(task); }
            }

            if (LocalScriptTasks != null)
            {
                var tasks = this.LocalScriptTasks.Where(x => x.EnviromentId == originalEnviromentId && !x.Deleted).Select(x => new LocalScriptTask()
                {
                    ApplicationId = x.ApplicationId,
                    EnviromentId = enviromentId,
                    Name = x.Name,
                    Position = this.NewTaskNumber(enviromentId),
                    Script = x.Script
                }).ToList();

                foreach (var task in tasks) { this.LocalScriptTasks.Add(task); }
            }

            if (RemoteScriptTasks != null)
            {
                var tasks = this.RemoteScriptTasks.Where(x => x.EnviromentId == originalEnviromentId && !x.Deleted).Select(x => new RemoteScriptTask()
                {
                    AgentId = machineId,
                    EnviromentId = enviromentId,
                    Folder = x.Folder,
                    Name = x.Name,
                    Position = this.NewTaskNumber(enviromentId),
                    Script = x.Script,
                    ApplicationId = x.ApplicationId
                }).ToList();

                foreach (var task in tasks) { this.RemoteScriptTasks.Add(task); }
            }

            if (TemplatedTasks != null)
            {
                var tasks = this.TemplatedTasks.Where(x => x.EnviromentId == originalEnviromentId && !x.Deleted).Select(x => new TemplatedTask()
                {
                    Deleted =false,
                    TaskTemplateVersionId = x.TaskTemplateVersionId,
                    AgentId = machineId,
                    EnviromentId = enviromentId,
                    Name = x.Name,
                    Position = this.NewTaskNumber(enviromentId),
                    ApplicationId = x.ApplicationId
                }).ToList();

                foreach (var task in tasks) { this.TemplatedTasks.Add(task); }
            }
        }

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
                Repository = repository,
                Position = this.NewTaskNumber(enviromentId)
            });
        }

        public virtual void ModifyDatabaseTask(int taskId, string name, string connectionString, string package, string dbName, string repository, int agentId, bool partial)
        {
            var task = this.DatabaseTasks.Single(x => x.Id == taskId);

            task.AgentId = agentId;
            task.ConnectionString = connectionString;
            task.DatabaseName = dbName;
            task.Name = name;
            task.Package = package;
            task.Repository = repository;
            task.Partial = partial;
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
                AdditionalParameters = parameters,
                Position = this.NewTaskNumber(enviromentId)
            });
        }

        public virtual void ModifyDeployTask(int id, string name, int agentId, string package, string directory, string repo, string installScript, string uninstallScript, string configFile, string transform, string configuration, IList<Parameter> parameters)
        {
            var task = this.DeployTasks.Single(x => x.Id == id);

            task.AgentId = agentId;
            task.Configuration = string.IsNullOrWhiteSpace(configuration) ? null : configuration;
            task.ConfigurationFile = string.IsNullOrWhiteSpace(configFile) ? null : configFile;
            task.ConfigurationTransform = string.IsNullOrWhiteSpace(transform) ? null : transform;
            task.Directory = directory;
            task.InstallScript = string.IsNullOrWhiteSpace(installScript) ? null : installScript;
            task.Name = name;
            task.PackageName = package;
            task.Repository = repo;
            task.UninstallScript = string.IsNullOrWhiteSpace(uninstallScript) ? null : uninstallScript;

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
                Script = script,
                Position = this.NewTaskNumber(enviromentId)
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
                Text = text,
                Position = this.NewTaskNumber(enviromentId)
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

        private int NewTaskNumber(int enviromentId)
        {
            var a = (this.DatabaseTasks.Where(x => x.EnviromentId == enviromentId).OrderByDescending(x => x.Position).FirstOrDefault() ?? new DatabaseTask()).Position;
            var b = (this.DeployTasks.Where(x => x.EnviromentId == enviromentId).OrderByDescending(x => x.Position).FirstOrDefault() ?? new DeployTask()).Position;
            var c = (this.LocalScriptTasks.Where(x => x.EnviromentId == enviromentId).OrderByDescending(x => x.Position).FirstOrDefault() ?? new LocalScriptTask()).Position;
            var d = (this.MailTasks.Where(x => x.EnviromentId == enviromentId).OrderByDescending(x => x.Position).FirstOrDefault() ?? new MailTask()).Position;
            var e = (this.RemoteScriptTasks.Where(x => x.EnviromentId == enviromentId).OrderByDescending(x => x.Position).FirstOrDefault() ?? new RemoteScriptTask()).Position;
            var f = (this.TemplatedTasks.Where(x => x.EnviromentId == enviromentId).OrderByDescending(x => x.Position).FirstOrDefault() ?? new TemplatedTask()).Position;

            var position = (new int[] { a, b, c, d, e, f }).ToList().Max();

            return position + 1;
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
                Folder = folder,
                Position = this.NewTaskNumber(enviromentId)
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

        public virtual void DeleteTask(int taskId, TaskType type)
        {
            switch (type)
            {
                case TaskType.Deploy:
                    this.DeployTasks.Single(x => x.Id == taskId).Deleted = true;
                    break;
                case TaskType.Mail:
                    this.MailTasks.Single(x => x.Id == taskId).Deleted = true;
                    break;
                case TaskType.RemoteScript:
                    this.RemoteScriptTasks.Single(x => x.Id == taskId).Deleted = true;
                    break;
                case TaskType.LocalScript:
                    this.LocalScriptTasks.Single(x => x.Id == taskId).Deleted = true;
                    break;
                case TaskType.Database:
                    this.DatabaseTasks.Single(x => x.Id == taskId).Deleted = true;
                    break;
                case TaskType.Templated:
                    this.TemplatedTasks.Single(x => x.Id == taskId).Deleted = true;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Rename the application
        /// </summary>
        /// <param name="name">New name of the application</param>
        public virtual void Rename(string name)
        {
            Name = name;
        }

        public virtual void Delete()
        {
            Deleted = true;
        }
    }
}