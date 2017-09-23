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
            app.TemplatedTasks = new List<TemplatedTask>();

            foreach (var task in originalApp.DatabaseTasks.Where(x => !x.Deleted))
            {
                app.AddDatabaseTask(task.Name, task.ConnectionString, task.Package, task.DatabaseName, task.EnviromentId, task.Repository, task.AgentId);
            }

            foreach (var task in originalApp.DeployTasks.Where(x => !x.Deleted))
            {
                app.AddDeployTask(task.EnviromentId, task.Name, task.AgentId, task.PackageName, task.Directory, task.Repository, task.Configuration, new List<Parameter>());
            }

            foreach (var task in originalApp.LocalScriptTasks.Where(x => !x.Deleted))
            {
                app.CreateLocalScriptTask(task.EnviromentId, task.Script, task.Name);
            }

            foreach (var task in originalApp.MailTasks.Where(x => !x.Deleted))
            {
                app.CreateMailTask(task.EnviromentId, task.Name, task.Text, task.Recipients, task.Sender);
            }

            foreach (var task in originalApp.RemoteScriptTasks.Where(x => !x.Deleted))
            {
                app.CreateRemoteScriptTask(task.EnviromentId, task.Script, task.Name, task.Folder, task.AgentId);
            }

            foreach (var task in originalApp.TemplatedTasks.Where(x => !x.Deleted))
            {
                app.AddTemplatedTask(task.AgentId, task.EnviromentId, task.TaskTemplateVersionId, task.Name, task.Prameters);
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