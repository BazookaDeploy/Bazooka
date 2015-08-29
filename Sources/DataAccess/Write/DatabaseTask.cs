namespace DataAccess.Write
{
    public class DatabaseTask
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ConnectionString { get; set; }
        public virtual string Package { get; set; }
        public virtual string DatabaseName { get; set; }
        public virtual int EnviromentId { get; set; }

    }
}
