using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Web.Commands
{
    public class BusinessRuleValidator : IBusinessRuleValidator
    {
        public IKernel Container { get; set; }


        public ICollection<string> Validate<T>(T command) where T : ICommand
        {
            IBusinessRule rule = null;
            try
            {
                rule = Container.Resolve<IBusinessRule<T>>();
            }
            catch (Exception)
            {
                return new List<string>();
            }

            return rule.Validate(command).ToList();
        }
    }
}
