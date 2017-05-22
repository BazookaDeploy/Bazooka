﻿using DataAccess.Write;
using System.Linq;
using Web.Commands;

namespace Web.CommandHandlers
{
    public class CreateNewTemplatedTaskVersionHandler : CommandHandler<CreateNewTaskTemplateVersion>
    {
        public override void Apply(CreateNewTaskTemplateVersion command)
        {
            var task = Repository.Get<TaskTemplate>(command.TemplatedTaskId);
            task.CreateNewVersion(command.Script, command.Parameters.Select(x => new TaskTemplateParameter()
            {
                Encrypted = x.Encrypted,
                Name = x.Name,
                Optional = x.Optional
            }).ToList());
            Repository.Save(task);
        }
    }
}