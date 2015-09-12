using System.Linq;

namespace DataAccess.Read
{
    public interface IReadContext
    {
        IQueryable<T> Query<T>() where T : class;
    }
}
