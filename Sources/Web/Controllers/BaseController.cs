using System;
using System.Web.Http;
using System.Web.Security;
using Web.Commands;

namespace Web.Controllers
{
    public class BaseController : ApiController
    {
        public ICommandExecutor Worker { get; set; }

        protected ExecutionResult Execute<T>(T command) where T : ICommand
        {
            if (command.CurrentUserId == Guid.Empty)
            {
                var user = Membership.GetUser();
                if (user != null)
                {
                    command.CurrentUserId = (Guid)user.ProviderUserKey;
                }           
            }

            return Worker.Execute(command);
        }
    }
}
