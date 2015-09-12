using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Commands
{
    public interface ICommandExecutor
    {
        ExecutionResult Execute<T>(T command) where T : ICommand;
    }
}
