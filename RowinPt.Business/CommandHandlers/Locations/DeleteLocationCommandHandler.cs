using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.CommandHandlers.Locations
{
    internal sealed class DeleteLocationCommandHandler : ICommandHandler<DeleteCommand<Location>>
    {
        private readonly IReader<LocationModel> _locationReader;
        private readonly IRepository<ScheduleModel> _scheduleRepository;
        private readonly IRepository<ScheduleItemModel> _scheduleItemRepository;
        private readonly IRepository<AgendaModel> _agendaRepository;

        public DeleteLocationCommandHandler(
            IReader<LocationModel> locationReader,
            IRepository<ScheduleModel> scheduleRepository,
            IRepository<ScheduleItemModel> scheduleItemRepository,
            IRepository<AgendaModel> agendaRepository)
        {
            _locationReader = locationReader;
            _scheduleRepository = scheduleRepository;
            _scheduleItemRepository = scheduleItemRepository;
            _agendaRepository = agendaRepository;
        }

        public void Handle(DeleteCommand<Location> command)
        {
            var scheduleIds =
                from schedule in _scheduleRepository.Entities
                where schedule.LocationId == command.Id
                select schedule.Id;

            var scheduleItemIds =
                    from scheduleItem in _scheduleItemRepository.Entities
                    where scheduleIds.Contains(scheduleItem.ScheduleId)
                    select scheduleItem.Id;

            var agendaIds =
                from agenda in _agendaRepository.Entities
                where scheduleItemIds.Contains(agenda.ScheduleItemId)
                select agenda.Id;

            _agendaRepository.RemoveByIds(agendaIds);
            _scheduleItemRepository.RemoveByIds(scheduleItemIds);
            _scheduleRepository.RemoveByIds(scheduleIds);
            _locationReader.DeleteById(command.Id);
        }
    }
}
