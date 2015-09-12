using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Commands
{
    public interface IBusinessRule<T> : IBusinessRule where T : ICommand
    {
        ICollection<string> Validate(T command);
    }
}
