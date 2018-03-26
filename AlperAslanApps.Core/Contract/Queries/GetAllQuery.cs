using System.Collections.Generic;
using System.Diagnostics;

namespace AlperAslanApps.Core.Contract.Queries
{
    [DebuggerStepThrough]
    public class GetAllQuery<TModel> : IQuery<IEnumerable<TModel>>
        where TModel : class, IIdentifier
    {
    }
}
