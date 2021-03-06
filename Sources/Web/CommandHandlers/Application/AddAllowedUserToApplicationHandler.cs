﻿using DataAccess.Write;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class AddAllowedUserToApplicationHandler : CommandHandler<AddAllowedUserToApplication>
    {
        public override void Apply(AddAllowedUserToApplication command)
        {
            var application = Repository.Get<Application>(command.ApplicationId);
            application.AddAllowedUser(command.EnviromentId, command.UserId);
            Repository.Save<Application>(application);
        }
    }
}
