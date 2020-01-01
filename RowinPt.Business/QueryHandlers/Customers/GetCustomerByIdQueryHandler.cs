﻿using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.Customers
{
    internal sealed class GetCustomerByIdQueryHandler : IQueryHandler<GetByIdQuery<Customer>, Customer>
    {
        private readonly IReader<CustomerModel> _customerReader;
        private readonly IReader<SubscriptionModel> _subscriptionReader;

        public GetCustomerByIdQueryHandler(
            IReader<CustomerModel> customerReader,
            IReader<SubscriptionModel> subscriptionReader)
        {
            _customerReader = customerReader;
            _subscriptionReader = subscriptionReader;
        }

        public Customer Handle(GetByIdQuery<Customer> query)
        {
            var customer = _customerReader.GetById(query.Id);

            return new Customer
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
                Goal = customer.Goal,
                Subscriptions = _subscriptionReader.Entities.Where(s => s.CustomerId == customer.Id).Select(subscription =>
                    new Customer.Subscription
                    {
                        Credits = subscription.WeeklyCredits,
                        Notes = subscription.Notes,
                        CourseTypeId = subscription.CourseTypeId,
                        StartDate = subscription.StartDate,
                    }).ToArray()
            };
        }
    }
}
