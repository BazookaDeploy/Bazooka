namespace DataAccess.Read
{
    public class MaintenanceLogEntryDto
    {
        public virtual int Id { get; set; }

        public virtual int MaintenanceTaskId { get; set; }

        public virtual string Text { get; set; }
    }
}
