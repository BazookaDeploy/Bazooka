namespace Web.Commands
{
    public interface IRepository
    {
        T Get<T>(int id);

        void Save<T>(T aggregate);
    }
}
