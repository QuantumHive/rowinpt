using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.CommandHandlers.CourseTypes
{
    internal sealed class DeleteCourseTypeCommandHandler : ICommandHandler<DeleteCommand<CourseType>>
    {
        private readonly IReader<CourseTypeModel> _courseTypeReader;
        private readonly IRepository<CourseModel> _courseRepository;
        private readonly IRepository<ScheduleItemModel> _scheduleItemRepository;
        private readonly IRepository<AgendaModel> _agendaRepository;
        private readonly IRepository<SubscriptionModel> _subscriptionRepository;

        public DeleteCourseTypeCommandHandler(
            IReader<CourseTypeModel> courseTypeReader,
            IRepository<CourseModel> courseRepository,
            IRepository<ScheduleItemModel> scheduleItemRepository,
            IRepository<AgendaModel> agendaRepository,
            IRepository<SubscriptionModel> subscriptionRepository)
        {
            _courseTypeReader = courseTypeReader;
            _courseRepository = courseRepository;
            _scheduleItemRepository = scheduleItemRepository;
            _agendaRepository = agendaRepository;
            _subscriptionRepository = subscriptionRepository;
        }
        public void Handle(DeleteCommand<CourseType> command)
        {
            var courseIds =
                from course in _courseRepository.Entities
                where course.CourseTypeId == command.Id
                select course.Id;

            var scheduleItemIds =
                from item in _scheduleItemRepository.Entities
                where courseIds.Contains(item.CourseId)
                select item.Id;

            var agendaIds =
                from agenda in _agendaRepository.Entities
                where scheduleItemIds.Contains(agenda.Id)
                select agenda.Id;

            var subscriptionIds =
                from subscription in _subscriptionRepository.Entities
                where subscription.CourseTypeId == command.Id
                select subscription.Id;

            _courseRepository.RemoveByIds(courseIds);
            _scheduleItemRepository.RemoveByIds(scheduleItemIds);
            _agendaRepository.RemoveByIds(agendaIds);
            _subscriptionRepository.RemoveByIds(subscriptionIds);
            _courseTypeReader.DeleteById(command.Id);
        }
    }
}
