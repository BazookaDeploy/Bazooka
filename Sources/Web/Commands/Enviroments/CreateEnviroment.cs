using System.ComponentModel.DataAnnotations;

namespace Web.Commands
{
    public class CreateEnviroment : ICommand, ICanBeRunOnlyByAdministrator
    {
        [Required]
        public string Name { get; set; }
    }
}
