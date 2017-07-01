namespace Web.CommandHandlers
{
    using DataAccess.Read;
    using DataAccess.Write;
    using System;
    using System.Linq;
    using Web.Commands;

    public class UpdateTemplatedTaskHandler : CommandHandler<UpdateTemplatedTask>
    {
        public IReadContext db { get; set; }

        public override void Apply(UpdateTemplatedTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            var task = application.TemplatedTasks.Single(x => x.Id == command.Id);

            var temp = db.Query<TaskTemplateVersionDto>().Single(x => x.Id == task.TaskTemplateVersionId);
            var last = db.Query<TaskTemplateVersionDto>().Where(x => x.TaskTemplateId == temp.TaskTemplateId).OrderByDescending(x => x.Version).First();
            var parameters = db.Query<TaskTemplateParameterDto>().Where(x => x.TaskTemplateVersionId == last.Id).ToList();
            var oldparameters = db.Query<TaskTemplateParameterDto>().Where(x => x.TaskTemplateVersionId == task.TaskTemplateVersionId).ToList();

            application.UpdateTemplatedTask(command.Id, last.Id, oldparameters.Select(x =>  new Tuple<int, int>(x.Id,parameters.Single(z => z.Name == x.Name).Id)).ToList());
            Repository.Save<Application>(application);
        }
    }
}