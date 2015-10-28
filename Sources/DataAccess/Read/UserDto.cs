using System.ComponentModel.DataAnnotations;
namespace DataAccess.Read
{
    public class UserDto
    {
        [Key]
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public bool Administrator { get; set; }
    }
}
