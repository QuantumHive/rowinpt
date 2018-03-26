using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using Microsoft.EntityFrameworkCore;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.Agenda
{
    internal sealed class GetCustomerAgendaByIdQueryHandler : IQueryHandler<GetByIdQuery<CustomerAgenda>, CustomerAgenda>
    {
        private readonly IReader<AgendaModel> _agendaReader;

        public GetCustomerAgendaByIdQueryHandler(
            IReader<AgendaModel> agendaReader)
        {
            _agendaReader = agendaReader;
        }

        public CustomerAgenda Handle(GetByIdQuery<CustomerAgenda> query)
        {
            var result =
                from agenda in EagerLoadedAgenda()
                where agenda.Id == query.Id
                let item = agenda.ScheduleItem
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

            return result.Single();
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
