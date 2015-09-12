using System.Collections.Generic;

namespace Web.Commands
{
    public interface IBusinessRule
    {
        ICollection<string> Validate(object command);
    }
}
