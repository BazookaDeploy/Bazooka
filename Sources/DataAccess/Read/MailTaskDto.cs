namespace DataAccess.Read
{
    public class MailTaskDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string Recipients { get; set; }

        public string Sender { get; set; }

        public int EnviromentId { get; set; }

        public int ApplicationId { get; set; }
    }
}
