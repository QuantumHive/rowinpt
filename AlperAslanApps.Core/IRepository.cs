using System;

namespace AlperAslanApps.Core
{
    public interface IRepository<TEntity> : IReader<TEntity>
        where TEntity : class, IModel
    {
        void Add(TEntity entity);
        void RemoveById(Guid id);
    }
}
