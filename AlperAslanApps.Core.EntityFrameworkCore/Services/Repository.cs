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
        private readonly ICompanyContext _companyContext;

        public Repository(TContext dbContext,
            ICompanyContext companyContext)
        {
            DbContext = dbContext;
            _companyContext = companyContext;
        }

        public IQueryable<TModel> Entities => DbSet.Where(m => m.CompanyId == _companyContext.CompanyId);

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
