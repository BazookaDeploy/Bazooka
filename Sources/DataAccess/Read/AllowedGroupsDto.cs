using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Read
{
    public class AllowedGroupsDto
    {
        [Key]
        public int Id { get; set; }

        public int EnviromentId { get; set; }

        public string GroupId { get; set; }

        public string Name { get; set; }
    }
}
