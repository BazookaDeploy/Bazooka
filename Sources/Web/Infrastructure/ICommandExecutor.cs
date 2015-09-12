namespace Web.Commands
{
    public interface ICommandExecutor
    {
        ExecutionResult Execute<T>(T command) where T : ICommand;
    }
}
