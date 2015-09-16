using NHibernate;
namespace Web.Commands
{
    public class Repository : IRepository
    {
        public ISession Session { get; set; }

        public T Get<T>(int id)
        {
            return Session.Load<T>(id);
        }

        public void Save<T>(T aggregate)
        {
            Session.SaveOrUpdate(aggregate);
        }


        public void Commit()
        {
            Session.Flush();
        }
    }
}
