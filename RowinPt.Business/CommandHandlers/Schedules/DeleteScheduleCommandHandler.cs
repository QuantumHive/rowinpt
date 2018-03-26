using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.CommandHandlers.Schedules
{
    internal sealed class DeleteScheduleCommandHandler : ICommandHandler<DeleteCommand<Schedule>>
    {
        private readonly IReader<ScheduleModel> _scheduleReader;
        private readonly IRepository<ScheduleItemModel> _scheduleItemRepository;
        private readonly IRepository<AgendaModel> _agendaRepository;

        public DeleteScheduleCommandHandler(
            IReader<ScheduleModel> scheduleReader,
            IRepository<ScheduleItemModel> scheduleItemRepository,
            IRepository<AgendaModel> agendaRepository)
        {
            _scheduleReader = scheduleReader;
            _scheduleItemRepository = scheduleItemRepository;
            _agendaRepository = agendaRepository;
        }

        public void Handle(DeleteCommand<Schedule> command)
        {
            var scheduleItemIds =
                from scheduleItem in _scheduleItemRepository.Entities
                where scheduleItem.ScheduleId == command.Id
                select scheduleItem.Id;

            var agendaIds =
                from agenda in _agendaRepository.Entities
                where scheduleItemIds.Contains(agenda.ScheduleItemId)
                select agenda.Id;

            _agendaRepository.RemoveByIds(agendaIds);
            _scheduleItemRepository.RemoveByIds(scheduleItemIds);
            _scheduleReader.DeleteById(command.Id);
        }
    }
}
