using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.CommandHandlers.Schedules
{
    internal sealed class UpdateScheduleCommandHandler : ICommandHandler<UpdateCommand<Schedule>>
    {
        private readonly IReader<ScheduleModel> _scheduleReader;

        public UpdateScheduleCommandHandler(
            IReader<ScheduleModel> scheduleReader)
        {
            _scheduleReader = scheduleReader;
        }

        public void Handle(UpdateCommand<Schedule> command)
        {
            var schedule = _scheduleReader.GetById(command.Model.Id);

            schedule.Name = command.Model.Name;
            schedule.LocationId = command.Model.LocationId;
        }
    }
}
