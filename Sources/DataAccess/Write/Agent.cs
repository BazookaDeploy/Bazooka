namespace DataAccess.Write
{
    public class Agent
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Address { get; set; }

        public virtual int EnviromentId { get; set; }

        public virtual System.DateTime? LastStatusCheck { get; set; }

        public virtual bool LastCheck { get; set; }
    }
}
