using System.Collections.Generic;

namespace Web.Commands
{
    public abstract class BusinessRule<T> : IBusinessRule<T> where T : ICommand
    {
        public abstract IEnumerable<string> Validate(T command);

        public IEnumerable<string> Validate(object command)
        {
            return Validate((T)command);
        }
    }
}
