﻿namespace Web.Commands
{
    public class SetApplicationGroup : ICommand, ICanBeRunByApplicationAdministrator
    {
        public int ApplicationId { get; set; }

        public int? ApplicationGroupId { get; set; }
    }
}
