using AlperAslanApps.Core;
using Microsoft.EntityFrameworkCore;
using RowinPt.Contract.Queries.Agenda;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.Agenda
{
    internal sealed class GetAgendaQueryHandler : IQueryHandler<GetAgendaQuery, IEnumerable<Contract.Models.Agenda>>
    {
        private readonly IReader<ScheduleItemModel> _scheduleItemReader;

        public GetAgendaQueryHandler(
            IReader<ScheduleItemModel> scheduleItemReader)
        {
            _scheduleItemReader = scheduleItemReader;
        }

        public IEnumerable<Contract.Models.Agenda> Handle(GetAgendaQuery query)
        {
            return
                from item in EagerLoadedScheduleItems()
                where item.Date == query.Date
                where item.Schedule.LocationId == query.LocationId
                select new Contract.Models.Agenda
                {
                    Id = item.Id,
                    Course = item.Course.Name,
                    Date = item.Date,
                    Start = item.StartTime,
                    End = item.EndTime,
                    Trainer = item.PersonalTrainer == null ? string.Empty : item.PersonalTrainer.Name,
                    Location = item.Schedule.Location.Name,
                    Capacity = item.Course.Capacity,
                    Registrations = item.Agenda.Count(),
                };
        }

        private IQueryable<ScheduleItemModel> EagerLoadedScheduleItems()
        {
            var itemQuery = _scheduleItemReader.Entities;
            itemQuery.Include(i => i.PersonalTrainer);
            itemQuery.Include(i => i.Course.CourseType);
            itemQuery.Include(i => i.Schedule.Location);
            return itemQuery;
        }
    }
}
