
namespace Web.Infrastructure
{
    using Web.Commands;

    /// <summary>
    ///     Interface to decide if a command can execute
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        ///     Tells if a command can be executed
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <returns>Command can be executed</returns>
        bool CanExecute(ICommand command);
    }
}