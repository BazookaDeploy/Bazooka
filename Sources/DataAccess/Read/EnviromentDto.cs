using System;

namespace DataAccess.Read
{
    public class EnviromentDto
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public string Configuration { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public string UserName { get; set; }

        public Guid DeployKey { get; set; }
    }
}