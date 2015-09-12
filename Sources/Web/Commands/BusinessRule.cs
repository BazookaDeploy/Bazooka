using System.Collections.Generic;

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
