using AlperAslanApps.Core;
using Microsoft.EntityFrameworkCore;
using RowinPt.Contract.Commands.Plan;
using RowinPt.Domain;
using System;
using System.Linq;

namespace RowinPt.Business.CommandHandlers.Plan
{
    internal sealed class PlanNewScheduleItemForCustomerCommandHandler : ICommandHandler<PlanNewScheduleItemForCustomerCommand>
    {
        private readonly IRepository<AgendaModel> _agendaRepository;
        private readonly IReader<SubscriptionModel> _subscriptionReader;
        private readonly IReader<ScheduleItemModel> _scheduleItemReader;
        private readonly ITimeProvider _timeProvider;

        public PlanNewScheduleItemForCustomerCommandHandler(
            IRepository<AgendaModel> agendaRepository,
            IReader<SubscriptionModel> subscriptionReader,
            IReader<ScheduleItemModel> scheduleItemReader,
            ITimeProvider timeProvider)
        {
            _agendaRepository = agendaRepository;
            _subscriptionReader = subscriptionReader;
            _scheduleItemReader = scheduleItemReader;
            _timeProvider = timeProvider;
        }

        public void Handle(PlanNewScheduleItemForCustomerCommand command)
        {
            ThrowInvalidOperations(command);

            var agenda = new AgendaModel
            {
                CustomerId = command.CustomerId,
                ScheduleItemId = command.ScheduleItemId
            };

            _agendaRepository.Add(agenda);

            var scheduleItem = _scheduleItemReader.GetById(command.ScheduleItemId);
            var courseTypeId = scheduleItem.Course.CourseTypeId;
            var subscription = _subscriptionReader.Entities.Single(s => s.CustomerId == command.CustomerId && s.CourseTypeId == courseTypeId);
            if(subscription.RecentEntry == null || subscription.RecentEntry < scheduleItem.Date)
            {
                subscription.RecentEntry = _timeProvider.Today;
            }
        }

        private void ThrowInvalidOperations(PlanNewScheduleItemForCustomerCommand command)
        {
            var subscriptions = _subscriptionReader.Entities.Where(s => s.CustomerId == command.CustomerId);
            var courseTypeId = _scheduleItemReader.Entities.Include(i => i.Course).Single(i => i.Id == command.ScheduleItemId).Course.CourseTypeId;

            if(!subscriptions.Any(s => s.CourseTypeId == courseTypeId))
            {
                // some validators would throw because of implicit logic before we come to this point
                // this check should remain as backup
                throw new InvalidOperationException(
                    $"Customer {command.CustomerId} is not eligible to subscribe for a scheduleitem {command.ScheduleItemId}");
            }
        }
    }
}
