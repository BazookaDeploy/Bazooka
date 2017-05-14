namespace DataAccess.Read
{
    public class TaskTemplateVersionDto
    {

        public virtual int Id { get; set; }


        public virtual int TaskTemplateId { get; set; }


        public virtual string Script { get; set; }


        public virtual int Version { get; set; }
    }
}
