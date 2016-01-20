namespace Web.Commands.Application
{
    using DataAccess.Read;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DeleteTask : ICommand
    {
        public int ApplicationId { get; set; }

        public int TaskId { get; set; }

        public TaskType Type { get; set; }
    }
}
