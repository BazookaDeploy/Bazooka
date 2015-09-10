namespace Web.Commands
{
    public interface ICommandHandler
    {
        void Execute(ICommand command);
    }
}
