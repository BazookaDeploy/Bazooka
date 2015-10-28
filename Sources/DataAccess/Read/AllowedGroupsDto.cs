using System.ComponentModel.DataAnnotations;

namespace DataAccess.Read
{
    public class AllowedGroupsDto
    {
        [Key]
        public int Id { get; set; }

        public int EnviromentId { get; set; }

        public int ApplicationId { get; set; }

        public string GroupId { get; set; }

        public string Name { get; set; }
    }
}
