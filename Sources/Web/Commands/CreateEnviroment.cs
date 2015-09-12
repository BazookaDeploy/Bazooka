using System.ComponentModel.DataAnnotations;

namespace Web.Commands
{
    public class CreateEnviroment : ICommand
    {
        [Required]
        public string Name { get; set; }
    }
}
