using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.Customers
{
    internal sealed class GetAllCustomersQueryHandler : IQueryHandler<GetAllQuery<Customer>, IEnumerable<Customer>>
    {
        private readonly IReader<CustomerModel> _customerReader;
        private readonly IReader<SubscriptionModel> _subscriptionReader;

        public GetAllCustomersQueryHandler(
            IReader<CustomerModel> customerReader,
            IReader<SubscriptionModel> subscriptionReader)
        {
            _customerReader = customerReader;
            _subscriptionReader = subscriptionReader;
        }

        public IEnumerable<Customer> Handle(GetAllQuery<Customer> query)
        {
            return _customerReader.Entities
                .Select(customer => new Customer
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Number = customer.Number,
                    Sex = customer.Sex,
                    BirthDate = customer.BirthDate,
                    Length = customer.Length,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    EmailConfirmed = customer.EmailConfirmed,
                    Subscriptions = _subscriptionReader.Entities.Where(s => s.CustomerId == customer.Id).Select(subscription => 
                    new Customer.Subscription
                    {
                        Credits = subscription.WeeklyCredits,
                        Notes = subscription.Notes,
                        CourseTypeId = subscription.CourseTypeId,
                        StartDate = subscription.StartDate,
                    })
                });
        }
    }
}
