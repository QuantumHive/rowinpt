using AlperAslanApps.Core;
using Microsoft.EntityFrameworkCore;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.Agenda
{
    internal sealed class GetCustomerAgendaQueryHandler : IQueryHandler<GetCustomerAgendaQuery, IEnumerable<CustomerAgenda>>
    {
        private readonly IReader<AgendaModel> _agendaReader;
        private readonly ITimeProvider _timeProvider;

        public GetCustomerAgendaQueryHandler(
            IReader<AgendaModel> agendaReader,
            ITimeProvider timeProvider)
        {
            _agendaReader = agendaReader;
            _timeProvider = timeProvider;
        }
        public IEnumerable<CustomerAgenda> Handle(GetCustomerAgendaQuery query)
        {
            return
                from agenda in EagerLoadedAgenda()
                where agenda.CustomerId == query.CustomerId
                let item = agenda.ScheduleItem
                where item.Date >= _timeProvider.Today
                select new CustomerAgenda
                {
                    Id = agenda.Id,
                    Course = item.Course.Name,
                    Date = item.Date,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Trainer = item.PersonalTrainer == null ? string.Empty : item.PersonalTrainer.Name,
                    Location = item.Schedule.Location.Name,
                    Capacity = item.Course.Capacity,
                    Registrations = item.Agenda.Count(),
                };
        }

        private IQueryable<AgendaModel> EagerLoadedAgenda()
        {
            var agendaQuery = _agendaReader.Entities
                .Include(l => l.ScheduleItem);

            agendaQuery.ThenInclude(i => i.PersonalTrainer);
            agendaQuery.ThenInclude(i => i.Course.CourseType);
            agendaQuery.ThenInclude(i => i.Schedule.Location);

            return agendaQuery;
        }
    }
}
