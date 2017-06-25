namespace Web.CommandHandlers
{
    using DataAccess.Write;
    using System.Linq;
    using Web.Commands;

    public class CreateTemplatedTaskHandler : CommandHandler<CreateTemplatedTask>
    {
        public override void Apply(CreateTemplatedTask command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.AddTemplatedTask(command.AgentId, command.EnviromentId, command.TaskVersionId, command.Name, command.Parameters.Select(x => new DataAccess.Write.TemplatedTaskParameter()
            {
                TaskTemplateParameterId = x.TaskTemplateParameterId,
                Value = x.Value
            }));
            Repository.Save<Application>(application);
        }
    }
}