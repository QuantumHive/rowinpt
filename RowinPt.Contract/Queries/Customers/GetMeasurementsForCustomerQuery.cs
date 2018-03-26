using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using System;
using System.Collections.Generic;

namespace RowinPt.Contract.Queries.Customers
{
    public class GetMeasurementsForCustomerQuery : IQuery<IEnumerable<Measurement>>
    {
        public GetMeasurementsForCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }
}
