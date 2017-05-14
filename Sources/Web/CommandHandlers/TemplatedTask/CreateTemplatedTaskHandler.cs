namespace Web.CommandHandlers
{
    using DataAccess.Write;
    using System.Collections.Generic;
    using Web.Commands;

    public class CreateTemplatedTaskHandler : CommandHandler<CreateTemplatedTask>
    {
        public override void Apply(CreateTemplatedTask command)
        {
            var task = new TaskTemplate();
            task.Create(command.Name, command.Description);
            Repository.Save(task);
            task.CreateNewVersion(string.Empty, new List<TaskTemplateParameter>());
            Repository.Save(task);
        }
    }
}