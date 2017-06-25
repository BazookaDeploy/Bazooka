namespace Web.CommandHandlers
{
    using DataAccess.Write;
    using System.Linq;
    using Web.Commands;

    public class ModifyTemplatedTaskHandler : CommandHandler<ModifyTemplatedTask>
    {
        public override void Apply(ModifyTemplatedTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.ModifyTemplatedTask(command.Id,command.AgentId, command.EnviromentId, command.Parameters.Select(x => new DataAccess.Write.TemplatedTaskParameter()
            {
                TaskTemplateParameterId = x.TaskTemplateParameterId,
                Value = x.Value
            }));
            Repository.Save<Application>(application);
        }
    }
}