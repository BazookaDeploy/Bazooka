using System.Collections.Generic;

namespace Web.Commands
{
    public interface IBusinessRule<T> : IBusinessRule where T : ICommand
    {
        ICollection<string> Validate(T command);
    }
}
