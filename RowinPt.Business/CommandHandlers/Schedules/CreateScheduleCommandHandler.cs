using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.CommandHandlers.Schedules
{
    internal sealed class CreateScheduleCommandHandler : ICommandHandler<CreateCommand<Schedule>>
    {
        private readonly IRepository<ScheduleModel> _scheduleRepository;

        public CreateScheduleCommandHandler(IRepository<ScheduleModel> scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public void Handle(CreateCommand<Schedule> command)
        {
            _scheduleRepository.Add(new ScheduleModel
            {
                Id = command.Model.Id,
                Name = command.Model.Name,
                LocationId = command.Model.LocationId,
            });
        }
    }
}
