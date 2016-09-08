using DataAccess.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CloneApplicationFromExistingHandler : CommandHandler<CreateApplicationFromExisting>
    {
        public override void Apply(CreateApplicationFromExisting command)
        {
            var app = new Application()
            {
                Name = command.Name
            };
            Repository.Save(app);

            var originalApp = Repository.Get<Application>(command.OriginalApplicationId);
            app.ApplicationGroupId = originalApp.ApplicationGroupId;

            app.DatabaseTasks = new List<DatabaseTask>();
            app.DeployTasks = new List<DeployTask>();
            app.LocalScriptTasks = new List<LocalScriptTask>();
            app.MailTasks = new List<MailTask>();
            app.RemoteScriptTasks = new List<RemoteScriptTask>();
            foreach (var task in originalApp.DatabaseTasks)
            {
                app.AddDatabaseTask(task.Name, task.ConnectionString, task.Package, task.DatabaseName, task.EnviromentId, task.Repository, task.AgentId);
            }

            foreach (var task in originalApp.DeployTasks)
            {
                app.AddDeployTask(task.EnviromentId, task.Name, task.AgentId, task.PackageName, task.Directory, task.Repository, task.Configuration, task.AdditionalParameters.Select(x => new Parameter() {Encrypted = x.Encrypted, Key = x.Key, Value = x.Value }).ToList());
            }

            foreach (var task in originalApp.LocalScriptTasks)
            {
                app.CreateLocalScriptTask(task.EnviromentId, task.Script, task.Name);
            }

            foreach (var task in originalApp.MailTasks)
            {
                app.CreateMailTask(task.EnviromentId, task.Name, task.Text, task.Recipients, task.Sender);
            }

            foreach (var task in originalApp.RemoteScriptTasks)
            {
                app.CreateRemoteScriptTask(task.EnviromentId, task.Script, task.Name, task.Folder, task.AgentId);
            }

            app.Administrators = new List<ApplicationAdministrator>();
            foreach (var user in originalApp.Administrators)
            {
                app.AddAdministrator(Guid.Parse(user.UserId));
            }

            app.AllowedUsers = new List<AllowedUser>();
            foreach (var user in originalApp.AllowedUsers)
            {
                app.AddAllowedUser(user.EnviromentId, Guid.Parse(user.UserId));
            }

            app.AllowedGroups = new List<AllowedGroup>();
            foreach (var group in originalApp.AllowedGroups)
            {
                app.AddAllowedGroup(group.EnviromentId,group.GroupId);
            }

            Repository.Save(app);

        }
    }
}