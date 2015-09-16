using System;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Web.Commands;
using Microsoft.AspNet.Identity;
namespace Web.Controllers
{
    public class BaseController : ApiController
    {
        public ICommandExecutor Worker { get; set; }

        protected ExecutionResult Execute<T>(T command) where T : ICommand
        {
            if (command.CurrentUserId == Guid.Empty)
            {
                var id = HttpContext.Current.User.Identity.GetUserId();
                if (id != null)
                {
                    command.CurrentUserId = Guid.Parse(id);
                }           
            }

            return Worker.Execute(command);
        }
    }
}
