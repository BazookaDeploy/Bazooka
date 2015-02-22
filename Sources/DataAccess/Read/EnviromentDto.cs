using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Read
{
    public class EnviromentDto
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public string Configuration { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}