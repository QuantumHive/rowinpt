using System;
using System.Collections.Generic;
using System.Linq;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Models;
using Microsoft.EntityFrameworkCore;
using RowinPt.Contract.Commands.Plan;
using RowinPt.Domain;

namespace RowinPt.Business.Validators.Plan
{
    internal sealed class CustomerCannotSubscribeBeforeStartDateOrHasNoCredits : IValidator<PlanNewScheduleItemForCustomerCommand>
    {
        private readonly IReader<SubscriptionModel> _subscriptionReader;
        private readonly IReader<ScheduleItemModel> _scheduleItemReader;
        private readonly ITimeProvider _timeProvider;
        private readonly IReader<AgendaModel> _agendaReader;

        public CustomerCannotSubscribeBeforeStartDateOrHasNoCredits(
            IReader<SubscriptionModel> subscriptionReader,
            IReader<ScheduleItemModel> scheduleItemReader,
            ITimeProvider timeProvider,
            IReader<AgendaModel> agendaReader)
        {
            _subscriptionReader = subscriptionReader;
            _scheduleItemReader = scheduleItemReader;
            _timeProvider = timeProvider;
            _agendaReader = agendaReader;
        }

        public IEnumerable<ValidationObject> Validate(PlanNewScheduleItemForCustomerCommand instance)
        {
            var item = _scheduleItemReader.Entities.Include(i => i.Course).Single(i => i.Id == instance.ScheduleItemId);
            var subscription = _subscriptionReader.Entities.Single(s => s.CustomerId == instance.CustomerId && s.CourseTypeId == item.Course.CourseTypeId);

            if (subscription.StartDate > item.Date)
            {
                return new ValidationObject
                {
                    Message = "Je kunt je niet aanmelden voor deze les omdat je abonnement nog niet is ingegaan"
                }.ToSingle();
            }

            const int expiration = 12;
            var minimumDate = _timeProvider.Today.StartOfWeek().AddDays(-7 * expiration);
            var multiplier = Multiplier(subscription.StartDate, minimumDate, expiration);

            var registrations =
                from agenda in _agendaReader.Entities.Include(a => a.ScheduleItem.Course)
                where agenda.CustomerId == instance.CustomerId
                where agenda.ScheduleItem.Date >= minimumDate
                where agenda.ScheduleItem.Course.CourseTypeId == subscription.CourseTypeId
                select 0;

            if(registrations.Count() >= subscription.WeeklyCredits * multiplier)
            {
                return new ValidationObject
                {
                    Message = "Je kunt je niet meer aanmelden omdat je tegoed op is. Je krijgt volgende week nieuw tegoed."
                }.ToSingle();
            }

            return Enumerable.Empty<ValidationObject>();
        }

        private int Multiplier(DateTime startDate, DateTime minimumDate, int expiration)
        {
            var startDateStartOfWeek = startDate.StartOfWeek();

            var multiplier = expiration + 4; // + 4 weeks in the future
            if (minimumDate < startDateStartOfWeek)
            {
                var pointer = minimumDate.Date;
                while (pointer != startDateStartOfWeek)
                {
                    multiplier--;
                    pointer = pointer.AddDays(7);
                }
            }
            return multiplier;
        }
    }
}
