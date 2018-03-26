using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using Microsoft.EntityFrameworkCore;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.Agenda
{
    internal sealed class GetAgendaByIdQueryHandler : IQueryHandler<GetByIdQuery<Contract.Models.Agenda>, Contract.Models.Agenda>
    {
        private readonly IReader<ScheduleItemModel> _scheduleItemReader;

        public GetAgendaByIdQueryHandler(
            IReader<ScheduleItemModel> scheduleItemReader)
        {
            _scheduleItemReader = scheduleItemReader;
        }

        public Contract.Models.Agenda Handle(GetByIdQuery<Contract.Models.Agenda> query)
        {
            var result =
                from item in EagerLoadedScheduleItems()
                where item.Id == query.Id
                let users =
                    from agenda in item.Agenda
                    select new UserRegistration
                    {
                        Id = agenda.CustomerId,
                        Name = agenda.Customer.Name,
                    }
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
                    Users = users.ToArray(),
                };

            return result.Single();
        }

        private IQueryable<ScheduleItemModel> EagerLoadedScheduleItems()
        {
            var itemQuery = _scheduleItemReader.Entities;
            itemQuery.Include(i => i.PersonalTrainer);
            itemQuery.Include(i => i.Course.CourseType);
            itemQuery.Include(i => i.Schedule.Location);
            itemQuery.Include(i => i.Agenda).ThenInclude(a => a.Customer);
            return itemQuery;
        }
    }
}
