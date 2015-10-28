using System.Collections.Generic;

namespace Web.Commands
{
    public interface IBusinessRuleValidator
    {
        ICollection<string> Validate<T>(T command) where T : ICommand;
    }
}
