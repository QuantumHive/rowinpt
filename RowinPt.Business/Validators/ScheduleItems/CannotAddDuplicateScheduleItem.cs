using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using AlperAslanApps.Core.Models;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Linq;
using System.Collections.Generic;

namespace RowinPt.Business.Validators.ScheduleItems
{
    internal sealed class CannotAddDuplicateScheduleItem : IValidator<CreateCommand<ScheduleItem>>
    {
        private readonly IReader<ScheduleItemModel> _scheduleItemReader;
        private readonly ITimeProvider _timeProvider;

        public CannotAddDuplicateScheduleItem(
            IReader<ScheduleItemModel> scheduleItemReader,
            ITimeProvider timeProvider)
        {
            _scheduleItemReader = scheduleItemReader;
            _timeProvider = timeProvider;
        }

        public IEnumerable<ValidationObject> Validate(CreateCommand<ScheduleItem> instance)
        {
            var model = instance.Model;

            var datesInTheFuture =
                from item in _scheduleItemReader.Entities
                where item.Date >= _timeProvider.Today
                where item.CourseId == model.CourseId
                where item.PersonalTrainerId == model.PersonalTrainerId
                where item.StartTime == model.Start
                select item.Date;

            for (var i = 0; i <= model.Repeat; i++)
            {
                var date = model.Date.AddDays(7 * i);

                if(datesInTheFuture.Any(d => d == date))
                {
                    yield return new ValidationObject
                    {
                        Message = $"Deze les is al ingepland op {date.ToString("dd-MM-YYYY")}",
                    };
                    break;
                }
            }
        }
    }
}
