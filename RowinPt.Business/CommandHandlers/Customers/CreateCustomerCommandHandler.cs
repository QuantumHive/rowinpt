using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System;

namespace RowinPt.Business.CommandHandlers.Customers
{
    internal sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCommand<Customer>>
    {
        private readonly IRepository<CustomerModel> _customerRepository;
        private readonly IRepository<SubscriptionModel> _subscriptionRepository;

        public CreateCustomerCommandHandler(
            IRepository<CustomerModel> customerRepository,
            IRepository<SubscriptionModel> subscriptionRepository)
        {
            _customerRepository = customerRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public void Handle(CreateCommand<Customer> command)
        {
            var model = command.Model;

            var user = new CustomerModel
            {
                Id = model.Id,
                Name = model.Name,
                Number = model.Number,
                Sex = model.Sex,
                Length = model.Length,
                BirthDate = model.BirthDate,

                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            foreach(var subscription in command.Model.Subscriptions)
            {
                _subscriptionRepository.Add(new SubscriptionModel
                {
                    CustomerId = user.Id,
                    WeeklyCredits = subscription.Credits,
                    CourseTypeId = subscription.CourseTypeId,
                    Notes = subscription.Notes,
                    StartDate = subscription.StartDate,
                });
            }

            user.SecurityStamp = Guid.NewGuid();
            user.NormalizedEmail = model.Email.Normalize().ToUpperInvariant();
            user.EmailConfirmed = false;
            user.PasswordHash = null;

            _customerRepository.Add(user);
        }
    }
}
