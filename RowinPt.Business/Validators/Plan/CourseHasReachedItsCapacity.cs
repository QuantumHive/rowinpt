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
            var count = model.Agenda.Count();

            if(count >= model.Course.Capacity)
            {
                yield return new ValidationObject
                {
                    Message = "Deze les zit helaas al vol"
                };
            }
            else
            {
                var overlaps =
                    from item in _scheduleItemReader.Entities.Include(i => i.Agenda)
                    where item.Id != model.Id
                    where item.Date == model.Date
                    where item.CourseId == model.CourseId
                    where item.PersonalTrainerId == model.PersonalTrainerId
                    where (model.StartTime >= item.StartTime && model.StartTime < item.EndTime)
                    || (model.EndTime > item.StartTime && model.EndTime <= item.EndTime)
                    select new Overlap
                    {
                        Id = item.Id,
                        StartTime = item.StartTime,
                        EndTime = item.EndTime,
                        Registrations = item.Agenda.Count()
                    };

                foreach(var overlap in overlaps)
                {
                    var partialOverlaps = GetPartialOverlaps(overlap, overlaps);

                    if(count + overlap.Registrations + partialOverlaps.Sum(o => o.Registrations) >= model.Course.Capacity)
                    {
                        yield return new ValidationObject
                        {
                            Message = "Aanmelden voor deze les is niet mogelijk, omdat de les vol zit in combinatie met andere overlappende lessen"
                        };
                        break;
                    }
                }
            }
        }

        private IEnumerable<Overlap> GetPartialOverlaps(Overlap overlap, IEnumerable<Overlap> others) =>
            from other in others
            where other.Id != overlap.Id
            where (overlap.StartTime >= other.StartTime && overlap.StartTime < other.EndTime)
            || (overlap.EndTime > other.StartTime && overlap.EndTime <= other.EndTime)
            select other;

        private ScheduleItemModel GetScheduleItem(Guid id) => 
            _scheduleItemReader.Entities
            .Include(i => i.Agenda)
            .Include(i => i.Course)
            .Single(i => i.Id == id);

        private struct Overlap
        {
            public Guid Id { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public int Registrations { get; set; }
        }
    }
}
