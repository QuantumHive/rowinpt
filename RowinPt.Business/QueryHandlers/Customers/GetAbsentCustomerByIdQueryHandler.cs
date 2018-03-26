using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.Customers
{
    internal sealed class GetAbsentCustomerByIdQueryHandler : IQueryHandler<GetByIdQuery<AbsentCustomerDetails>, AbsentCustomerDetails>
    {
        private readonly IReader<SubscriptionModel> _subscriptionReader;
        private readonly IReader<AgendaModel> _agendaReader;
        private readonly IReader<CustomerModel> _customerReader;
        private readonly IReader<AbsenceNotesModel> _absenceNotesReader;
        private readonly IReader<UserModel> _userReader;

        public GetAbsentCustomerByIdQueryHandler(
            IReader<SubscriptionModel> subscriptionReader,
            IReader<AgendaModel> agendaReader,
            IReader<CustomerModel> customerReader,
            IReader<AbsenceNotesModel> absenceNotesReader,
            IReader<UserModel> userReader)
        {
            _subscriptionReader = subscriptionReader;
            _agendaReader = agendaReader;
            _customerReader = customerReader;
            _absenceNotesReader = absenceNotesReader;
            _userReader = userReader;
        }

        public AbsentCustomerDetails Handle(GetByIdQuery<AbsentCustomerDetails> query)
        {
            var activity =
                from subscription in _subscriptionReader.Entities
                where subscription.CustomerId == query.Id
                select new AbsentCustomerActivity
                {
                    CourseTypeId = subscription.CourseTypeId,
                    Subscription = subscription.CourseType.Name,
                    LastSeen = subscription.RecentEntry,
                };

            var customer = _customerReader.GetById(query.Id);
            var notes = _absenceNotesReader.Entities.SingleOrDefault(a => a.CustomerId == query.Id);
            var editedBy = notes != null ? _userReader.GetById(Guid.Parse(notes.EditedBy)).Name : null;

            return new AbsentCustomerDetails
            {
                Id = customer.Id,
                Email = customer.Email,
                Phone = customer.PhoneNumber,
                Name = customer.Name,
                Activity = activity.ToArray(),
                Notes = notes?.Notes ?? "",
                LastUpdatedBy = editedBy,
                LastUpdatedOn = notes?.EditedOn,
            };
        }
    }
}
