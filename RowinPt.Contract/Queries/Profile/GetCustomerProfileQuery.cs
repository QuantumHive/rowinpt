using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using System.Collections.Generic;

namespace RowinPt.Contract.Queries.Profile
{
    public class GetCustomerProfileQuery : UserQuery, IQuery<IEnumerable<Measurement>>
    {
    }
}
