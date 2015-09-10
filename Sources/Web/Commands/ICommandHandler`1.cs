namespace Web.Commands
{
    public interface ICommandHandler<T> : ICommandHandler where T : ICommand
    {
        void Execute(T command);
    }
}
