using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using System.Collections.Generic;

namespace RowinPt.Contract.Queries.Customers
{
    public class GetAbsentCustomersByWeekQuery : IQuery<IEnumerable<AbsentCustomer>>
    {
        public GetAbsentCustomersByWeekQuery(int weeks)
        {
            Weeks = weeks;
        }

        public int Weeks { get; }
    }
}
