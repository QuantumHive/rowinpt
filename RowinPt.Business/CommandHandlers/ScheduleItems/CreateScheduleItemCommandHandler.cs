using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.CommandHandlers.Schedules
{
    internal sealed class CreateScheduleItemCommandHandler : ICommandHandler<CreateCommand<ScheduleItem>>
    {
        private readonly IRepository<ScheduleItemModel> _scheduleItemRepository;

        public CreateScheduleItemCommandHandler(IRepository<ScheduleItemModel> scheduleItemRepository)
        {
            _scheduleItemRepository = scheduleItemRepository;
        }

        public void Handle(CreateCommand<ScheduleItem> command)
        {
            _scheduleItemRepository.Add(CreateModel(command.Model));

            for(var i = 1; i <= command.Model.Repeat; i++)
            {
                var model = CreateModel(command.Model);
                model.Date = model.Date.AddDays(7 * i);
                _scheduleItemRepository.Add(model);
            }
        }

        private ScheduleItemModel CreateModel(ScheduleItem model) => new ScheduleItemModel
        {
            ScheduleId = model.ScheduleId,
            PersonalTrainerId = model.PersonalTrainerId,
            CourseId = model.CourseId,
            StartTime = model.Start,
            EndTime = model.End,
            Date = model.Date,
        };
    }
}
