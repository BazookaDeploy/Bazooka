using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Read
{
    public class DeployerDto
    {

        public int EnviromentId { get; set; }

        public string UserId { get; set; }

        public int ApplicationId { get; set; }

        public string UserName { get; set; }

        public string ApplicationName { get; set; }

        public string Configuration { get; set; }
    }
}
