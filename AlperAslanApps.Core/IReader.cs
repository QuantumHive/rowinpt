using System.Linq;

namespace AlperAslanApps.Core
{
    public interface IReader<TEntity>
        where TEntity : class, IModel
    {
        IQueryable<TEntity> Entities { get; }
    }
}
