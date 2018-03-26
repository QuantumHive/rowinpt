using System;
using System.Diagnostics;

namespace AlperAslanApps.Core.Contract.Queries
{
    [DebuggerStepThrough]
    public class GetByIdQuery<TModel> : IQuery<TModel>
        where TModel : class, IIdentifier
    {
        public GetByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
