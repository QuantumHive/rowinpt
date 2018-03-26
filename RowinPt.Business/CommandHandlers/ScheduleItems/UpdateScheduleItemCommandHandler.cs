using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.CommandHandlers.Schedules
{
    internal sealed class UpdateScheduleItemCommandHandler : ICommandHandler<UpdateCommand<ScheduleItem>>
    {
        private readonly IReader<ScheduleItemModel> _scheduleItemReader;
        public UpdateScheduleItemCommandHandler(
            IReader<ScheduleItemModel> scheduleItemReader)
        {
            _scheduleItemReader = scheduleItemReader;
        }

        public void Handle(UpdateCommand<ScheduleItem> command)
        {
            var item = _scheduleItemReader.GetById(command.Model.Id);

            item.Date = command.Model.Date;
            item.StartTime = command.Model.Start;
            item.EndTime = command.Model.End;
            item.CourseId = command.Model.CourseId;
            item.PersonalTrainerId = command.Model.PersonalTrainerId;
        }
    }
}
