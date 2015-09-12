using Castle.MicroKernel;
using Castle.Windsor;

namespace Web.Commands
{
    public class CommandExecutor : ICommandExecutor
    {
        public IKernel Container { get; set; }

        public ExecutionResult Execute<T>(T command) where T : ICommand
        {
            var handler = Container.Resolve<ICommandHandler<T>>();
            return handler.Execute(command);
        }
    }
}
