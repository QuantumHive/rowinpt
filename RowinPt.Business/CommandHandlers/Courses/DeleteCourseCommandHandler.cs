using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.CommandHandlers.Courses
{
    internal sealed class DeleteCourseCommandHandler : ICommandHandler<DeleteCommand<Course>>
    {
        private readonly IReader<CourseModel> _courseReader;
        private readonly IRepository<ScheduleItemModel> _scheduleItemRepository;
        private readonly IRepository<AgendaModel> _agendaRepository;

        public DeleteCourseCommandHandler(
            IReader<CourseModel> courseReader,
            IRepository<ScheduleItemModel> scheduleItemRepository,
            IRepository<AgendaModel> agendaRepository)
        {
            _courseReader = courseReader;
            _scheduleItemRepository = scheduleItemRepository;
            _agendaRepository = agendaRepository;
        }
        public void Handle(DeleteCommand<Course> command)
        {
            var scheduleItemIds =
                from scheduleItem in _scheduleItemRepository.Entities
                where scheduleItem.CourseId == command.Id
                select scheduleItem.Id;

            var agendaIds =
                from agenda in _agendaRepository.Entities
                where scheduleItemIds.Contains(agenda.Id)
                select agenda.Id;

            _agendaRepository.RemoveByIds(agendaIds);
            _scheduleItemRepository.RemoveByIds(scheduleItemIds);
            _courseReader.DeleteById(command.Id);
        }
    }
}
