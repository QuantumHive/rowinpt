using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.ScheduleItems
{
    internal sealed class GetAllScheduleItemsByScheduleIdQueryHandler : IQueryHandler<GetAllScheduleItemsByScheduleIdQuery, IEnumerable<ScheduleItem>>
    {
        private readonly IReader<ScheduleItemModel> _schedulteItemReader;
        private readonly ITimeProvider _timeProvider;

        public GetAllScheduleItemsByScheduleIdQueryHandler(
            IReader<ScheduleItemModel> schedulteItemReader,
            ITimeProvider timeProvider)
        {
            _schedulteItemReader = schedulteItemReader;
            _timeProvider = timeProvider;
        }

        public IEnumerable<ScheduleItem> Handle(GetAllScheduleItemsByScheduleIdQuery query)
        {
            return
                from item in _schedulteItemReader.Entities
                where item.ScheduleId == query.ScheduleId
                where item.Date >= _timeProvider.Today
                select new ScheduleItem
                {
                    Id = item.Id,
                    Date = item.Date,
                    Start = item.StartTime,
                    End = item.EndTime,
                    ScheduleId = item.ScheduleId,
                    PersonalTrainerId = item.PersonalTrainerId,
                    CourseId = item.CourseId,
                };
        }
    }
}
