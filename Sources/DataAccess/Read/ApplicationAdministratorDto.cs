namespace DataAccess.Read
{
    using System.ComponentModel.DataAnnotations;

    public class ApplicationAdministratorDto
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ApplicationId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }
    }
}
