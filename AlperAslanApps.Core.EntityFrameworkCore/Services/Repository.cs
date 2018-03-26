using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace AlperAslanApps.Core.EntityFrameworkCore.Services
{
    [DebuggerStepThrough]
    public class Repository<TContext, TModel> : IRepository<TModel>
            where TContext : DbContext
            where TModel : class, IModel, new()
    {
        protected readonly TContext DbContext;

        public Repository(TContext dbContext) => DbContext = dbContext;

        public IQueryable<TModel> Entities => DbSet;

        public void Add(TModel entity) => DbSet.Add(entity);

        public void RemoveById(Guid id)
        {
            var model = new TModel
            {
                Id = id,
                Active = false
            };

            DbSet.Attach(model);
            DbContext.Entry(model).Property(nameof(IModel.Active)).IsModified = true;
        }

        private DbSet<TModel> DbSet => DbContext.Set<TModel>();
    }
}
