using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Commands
{
    public class CreateEnviroment : ICommand
    {
        [Required]
        public string Name { get; set; }
    }
}
