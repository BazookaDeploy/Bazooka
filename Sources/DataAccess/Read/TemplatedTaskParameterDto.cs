namespace DataAccess.Read
{
    public class TemplatedTaskParameterDto
    {
        public virtual int Id { get; set; }

        public virtual int TaskTemplateParameterId { get; set; }

        public virtual int TemplatedTaskId { get; set; }

        public virtual string Value { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Optional { get; set; }

        public virtual bool Encrypted { get; set; }
    }
}
