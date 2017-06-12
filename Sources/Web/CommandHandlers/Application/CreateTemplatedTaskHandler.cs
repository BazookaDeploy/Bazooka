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
            application.AddTemplatedTask(command.AgentId, command.EnviromentId, command.Version, command.Name, command.Parameters.Select(x => new Parameter()
            {
                ParameterId = x.ParameterId,
                Value = x.Value
            }));
            Repository.Save<Application>(application);
        }
    }
}