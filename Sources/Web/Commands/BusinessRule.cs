using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Commands
{
    public abstract class BusinessRule<T> : IBusinessRule<T> where T : ICommand
    {
        public abstract ICollection<string> Validate(T command);

        public ICollection<string> Validate(object command)
        {
            return Validate((T)command);
        }
    }
}
