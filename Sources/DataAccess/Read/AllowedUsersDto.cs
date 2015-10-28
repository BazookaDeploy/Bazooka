using System.ComponentModel.DataAnnotations;

namespace DataAccess.Read
{
    public class AllowedUsersDto
    {
        [Key]
        public int Id { get; set; }

        public string USerId { get; set; }

        public int EnviromentId { get; set; }

        public int ApplicationId { get; set; }

        public string UserName { get; set; }
    }
}
