using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.CommandHandlers.Schedules
{
    internal sealed class DeleteScheduleItemCommandHandler : ICommandHandler<DeleteCommand<ScheduleItem>>
    {
        private readonly IReader<ScheduleItemModel> _scheduleItemReader;
        private readonly IRepository<AgendaModel> _agendaRepository;

        public DeleteScheduleItemCommandHandler(
            IReader<ScheduleItemModel> scheduleItemReader,
            IRepository<AgendaModel> agendaRepository)
        {
            _scheduleItemReader = scheduleItemReader;
            _agendaRepository = agendaRepository;
        }

        public void Handle(DeleteCommand<ScheduleItem> command)
        {
            var agendaIds =
                from agenda in _agendaRepository.Entities
                where agenda.ScheduleItemId == command.Id
                select agenda.Id;

            _agendaRepository.RemoveByIds(agendaIds);
            _scheduleItemReader.DeleteById(command.Id);
        }
    }
}
