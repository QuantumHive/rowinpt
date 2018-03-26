using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using AlperAslanApps.Core.Models;
using Microsoft.EntityFrameworkCore;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.Validators.ScheduleItems
{
    internal sealed class CannotChangeScheduleItemToDifferentCourseTypeIfCustomersSubscribed
        : IValidator<UpdateCommand<ScheduleItem>>
    {
        private readonly IReader<AgendaModel> _agendaReader;
        private readonly IReader<ScheduleItemModel> _scheduleItemReader;
        private readonly IReader<CourseModel> _courseReader;

        public CannotChangeScheduleItemToDifferentCourseTypeIfCustomersSubscribed(
            IReader<AgendaModel> agendaReader,
            IReader<ScheduleItemModel> scheduleItemReader,
            IReader<CourseModel> courseReader)
        {
            _agendaReader = agendaReader;
            _scheduleItemReader = scheduleItemReader;
            _courseReader = courseReader;
        }

        public IEnumerable<ValidationObject> Validate(UpdateCommand<ScheduleItem> instance)
        {
            var agenda = _agendaReader.Entities.Where(a => a.ScheduleItemId == instance.Model.Id);

            if (agenda.Any())
            {
                var item = _scheduleItemReader.Entities.Include(i => i.Course).Single(i => i.Id == instance.Model.Id);
                var newCourseTypeId = _courseReader.GetById(instance.Model.CourseId).CourseTypeId;

                if(newCourseTypeId != item.Course.CourseTypeId)
                {
                    yield return new ValidationObject
                    {
                        Message = "Je kunt de lesvorm van deze planning item niet veranderen, omdat er al aanmeldingen zijn."
                    };
                }
            }
        }
    }
}
