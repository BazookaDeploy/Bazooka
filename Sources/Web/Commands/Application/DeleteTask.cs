namespace Web.Commands.Application
{
    using DataAccess.Read;

    public class DeleteTask : ICommand, ICanBeRunByApplicationAdministrator
    {
        public int ApplicationId { get; set; }

        public int TaskId { get; set; }

        public TaskType Type { get; set; }
    }
}
