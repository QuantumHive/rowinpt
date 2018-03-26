using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AlperAslanApps.Core
{
    public static class RepositoryExtensions
    {
        [DebuggerStepThrough]
        public static TModel GetById<TModel>(this IReader<TModel> reader, Guid id)
            where TModel : class, IModel =>
            reader.Entities.Single(m => m.Id == id);

        [DebuggerStepThrough]
        public static void DeleteById<TModel>(this IReader<TModel> reader, Guid id)
            where TModel : class, IModel =>
            reader.GetById(id).Active = false;

        [DebuggerStepThrough]
        public static void RemoveByIds<TModel>(this IRepository<TModel> repository, IEnumerable<Guid> ids)
            where TModel : class, IModel
        {
            foreach(var id in ids)
            {
                repository.RemoveById(id);
            }
        }
    }
}
