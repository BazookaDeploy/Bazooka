namespace DataAccess.Read
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UsersInGroupsDto
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public string RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
