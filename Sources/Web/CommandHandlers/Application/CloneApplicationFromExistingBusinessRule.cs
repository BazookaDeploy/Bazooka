namespace Web.CommandHandlers
{
    using DataAccess.Read;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Web.Commands;

    public class CloneApplicationFromExistingBusinessRule : BusinessRule<CreateApplicationFromExisting>
    {
        public IReadContext ReadContext { get; set; }


        public override IEnumerable<string> Validate(CreateApplicationFromExisting command)
        {
            var user = ReadContext.Query<UserDto>().SingleOrDefault(x => x.Id == command.CurrentUserId.ToString());

            if (user == null)
            {
                yield return "Only authorized users can create an application";
                yield break;
            }

            if (!user.Administrator)
            {
                yield return "Only an administrator can create an application";
                yield break;
            }

            var apps = ReadContext.Query<ApplicationDto>().Count(x => x.Name == command.Name);
            if (apps > 0)
            {
                yield return "The application name must be unique";
                yield break;
            }
        }
    }
}