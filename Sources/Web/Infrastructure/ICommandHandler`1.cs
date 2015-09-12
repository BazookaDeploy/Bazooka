namespace Web.Commands
{
    public interface ICommandHandler<T> : ICommandHandler where T : ICommand
    {
        ExecutionResult Execute(T command);
    }
}
