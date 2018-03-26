using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.CommandHandlers.Customers
{
    internal sealed class UpdateCustomerCommandHandler : ICommandHandler<UpdateCommand<Customer>>
    {
        private readonly IReader<CustomerModel> _customerReader;
        private readonly IReader<SubscriptionModel> _subscriptionReader;
        private readonly IRepository<SubscriptionModel> _subscriptionRepository;

        public UpdateCustomerCommandHandler(
            IReader<CustomerModel> customerReader,
            IReader<SubscriptionModel> subscriptionReader,
            IRepository<SubscriptionModel> subscriptionRepository)
        {
            _customerReader = customerReader;
            _subscriptionReader = subscriptionReader;
            _subscriptionRepository = subscriptionRepository;
        }

        public void Handle(UpdateCommand<Customer> command)
        {
            var customer = _customerReader.GetById(command.Model.Id);

            customer.Name = command.Model.Name;
            customer.PhoneNumber = command.Model.PhoneNumber;
            customer.Sex = command.Model.Sex;
            customer.Number = command.Model.Number;
            customer.BirthDate = command.Model.BirthDate;
            customer.Length = command.Model.Length;

            UpdateSubscriptions(customer, command);

            if (!customer.EmailConfirmed)
            {
                customer.Email = command.Model.Email;
            }
        }

        private void UpdateSubscriptions(CustomerModel customer, UpdateCommand<Customer> command)
        {
            var deletedSubscriptions =
                from subscription in Subscriptions(customer.Id)
                where !command.Model.Subscriptions.Any(s => s.CourseTypeId == subscription.CourseTypeId)
                select subscription.Id;

            var addedSubscriptions =
                from subscription in command.Model.Subscriptions
                where !Subscriptions(customer.Id).Any(s => s.CourseTypeId == subscription.CourseTypeId)
                select subscription;

            var editedSubscriptions =
                from subscription in Subscriptions(customer.Id)
                where command.Model.Subscriptions.Any(s => s.CourseTypeId == subscription.CourseTypeId)
                select subscription;

            foreach (var subscriptionId in deletedSubscriptions)
            {
                _subscriptionReader.DeleteById(subscriptionId);
            }

            foreach (var subscription in addedSubscriptions)
            {
                _subscriptionRepository.Add(new SubscriptionModel
                {
                    CustomerId = customer.Id,
                    WeeklyCredits = subscription.Credits,
                    CourseTypeId = subscription.CourseTypeId,
                    Notes = subscription.Notes,
                    StartDate = subscription.StartDate,
                });
            }

            foreach (var subscription in editedSubscriptions)
            {
                var edit = command.Model.Subscriptions.Single(s => s.CourseTypeId == subscription.CourseTypeId);
                subscription.WeeklyCredits = edit.Credits;
                subscription.Notes = edit.Notes;
                subscription.StartDate = edit.StartDate;
            }
        }

        private IEnumerable<SubscriptionModel> Subscriptions(Guid customerId) => 
            _subscriptionReader.Entities.Where(s => s.CustomerId == customerId);
    }
}
