using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.QueryHandlers.ScheduleItems
{
    internal sealed class GetScheduleItemByIdQueryHandler : IQueryHandler<GetByIdQuery<ScheduleItem>, ScheduleItem>
    {
        private readonly IReader<ScheduleItemModel> _scheduleItemReader;

        public GetScheduleItemByIdQueryHandler(
            IReader<ScheduleItemModel> scheduleItemReader)
        {
            _scheduleItemReader = scheduleItemReader;
        }

        public ScheduleItem Handle(GetByIdQuery<ScheduleItem> query)
        {
            var item = _scheduleItemReader.GetById(query.Id);

            return new ScheduleItem
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
