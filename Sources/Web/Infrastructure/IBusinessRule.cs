using System.Collections.Generic;

namespace Web.Commands
{
    public interface IBusinessRule
    {
        IEnumerable<string> Validate(object command);
    }
}
