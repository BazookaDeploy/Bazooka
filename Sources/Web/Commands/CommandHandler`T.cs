using System;

namespace Web.Commands
{
    public abstract class CommandHandler<T> : ICommandHandler<T> where T : ICommand
    {
        public IBusinessRuleValidator BusinessRuleValidator { get; set; }

        public IRepository Repository { get; set; }

        public abstract void Apply(T command);

        public ExecutionResult Execute(T command)
        {
            var results = BusinessRuleValidator.Validate(command);

            if (results.Count > 0)
            {
                return new ExecutionResult()
                {
                    Success = false,
                    Errors = results
                };
            }

            try
            {
                this.Apply(command);
                return new ExecutionResult() { Success = true };
            }
            catch (Exception e)
            {
                return new ExecutionResult()
                {
                    Success = false,
                    Errors = new string[] { e.Message }
                };
            }

        }

        public ExecutionResult Execute(ICommand command)
        {
            return Execute((T)command);
        }

    }
}
