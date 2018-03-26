using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries.Customers;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.Customers
{
    internal sealed class GetAbsentCustomersByWeekQueryHandler : IQueryHandler<GetAbsentCustomersByWeekQuery, IEnumerable<AbsentCustomer>>
    {
        private readonly IReader<CustomerModel> _customerReader;
        private readonly ITimeProvider _timeProvider;

        public GetAbsentCustomersByWeekQueryHandler(
            IReader<CustomerModel> customerReader,
            ITimeProvider timeProvider)
        {
            _customerReader = customerReader;
            _timeProvider = timeProvider;
        }

        public IEnumerable<AbsentCustomer> Handle(GetAbsentCustomersByWeekQuery query)
        {
            var absentDate = _timeProvider.Today.AddDays(-7 * query.Weeks);

            return
                from customer in _customerReader.Entities
                where customer.EmailConfirmed

                let lastSeen = (
                    from subscription in customer.Subscriptions
                    let recentEntry = subscription.RecentEntry
                    where recentEntry == null
                    || recentEntry < absentDate
                    orderby recentEntry.HasValue, recentEntry descending
                    select recentEntry).ToArray()

                where lastSeen.Any()

                select new AbsentCustomer
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    LastSeen = lastSeen.First()
                };
        }
    }
}
