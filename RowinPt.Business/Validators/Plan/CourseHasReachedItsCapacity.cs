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
    internal sealed class CourseHasReachedItsCapacity : IValidator<PlanNewScheduleItemForCustomerCommand>
    {
        private readonly IReader<ScheduleItemModel> _scheduleItemReader;

        public CourseHasReachedItsCapacity(
            IReader<ScheduleItemModel> scheduleItemReader)
        {
            _scheduleItemReader = scheduleItemReader;
        }

        public IEnumerable<ValidationObject> Validate(PlanNewScheduleItemForCustomerCommand instance)
        {
            var model = GetScheduleItem(instance.ScheduleItemId);

            if(model.Agenda.Count() >= model.Course.Capacity)
            {
                yield return new ValidationObject
                {
                    Message = "Deze les zit helaas al vol"
                };
            }
            else
            {
                var potentialOverlaps =
                    from item in _scheduleItemReader.Entities.Include(i => i.Agenda)
                    where item.Id != model.Id
                    where item.Date == model.Date
                    where item.CourseId == model.CourseId
                    where item.PersonalTrainerId == model.PersonalTrainerId
                    select new
                    {
                        item.StartTime,
                        item.EndTime,
                        Registrations = item.Agenda.Count()
                    };

                var overlaps =
                    from potential in potentialOverlaps
                    where (model.StartTime >= potential.StartTime && model.StartTime < potential.EndTime)
                    || (model.EndTime > potential.StartTime && model.EndTime <= potential.EndTime)
                    select potential.Registrations;

                if(model.Agenda.Count() + overlaps.Sum() >= model.Course.Capacity)
                {
                    yield return new ValidationObject
                    {
                        Message = "Aanmelden voor deze les is niet mogelijk, omdat de les vol zit in combinatie met andere overlappende lessen"
                    };
                }
            }
        }

        private ScheduleItemModel GetScheduleItem(Guid id) => 
            _scheduleItemReader.Entities
            .Include(i => i.Agenda)
            .Include(i => i.Course)
            .Single(i => i.Id == id);
    }
}
