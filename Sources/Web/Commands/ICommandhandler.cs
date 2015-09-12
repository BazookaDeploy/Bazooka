namespace Web.Commands
{
    public interface ICommandHandler
    {
        ExecutionResult Execute(ICommand command);
    }
}
