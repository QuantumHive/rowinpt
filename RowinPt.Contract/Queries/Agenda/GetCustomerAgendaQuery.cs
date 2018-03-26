using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using System;
using System.Collections.Generic;

namespace RowinPt.Contract.Queries
{
    public class GetCustomerAgendaQuery : IQuery<IEnumerable<CustomerAgenda>>
    {
        public GetCustomerAgendaQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }
}
