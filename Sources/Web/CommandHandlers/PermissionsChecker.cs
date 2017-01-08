using DataAccess.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Infrastructure;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class PermissionsChecker : IPermissionChecker
    {
        public IReadContext ReadContext { get; set; }

        public bool CanExecute(ICommand command)
        {
            if (command is ICanBeRunOnlyByAdministrator)
            {
                var user = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

                if (user == null)
                {
                    return false;
                }

                if (!user.Administrator)
                {
                    return false;
                }
            }

            if (command is ICanBeRunByApplicationAdministrator)
            {
                var user2 = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

                if (user2.Administrator)
                {
                    return true;
                }

                var c2 = command as ICanBeRunByApplicationAdministrator;

                var user = ReadContext.Query<ApplicationAdministratorDto>().Where(x => x.ApplicationId == c2.ApplicationId && x.UserId == command.CurrentUserId.ToString());

                if (user.Count() == 0)
                {
                    return false;
                }
            }


            return true;
        }
    }
}