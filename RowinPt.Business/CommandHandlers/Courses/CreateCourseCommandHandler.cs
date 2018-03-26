using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.CommandHandlers.Courses
{
    internal sealed class CreateCourseCommandHandler : ICommandHandler<CreateCommand<Course>>
    {
        private readonly IRepository<CourseModel> _courseRepository;

        public CreateCourseCommandHandler(IRepository<CourseModel> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Handle(CreateCommand<Course> command)
        {
            var course = new CourseModel
            {
                Id = command.Model.Id,
                Name = command.Model.Name,
                Capacity = command.Model.Capacity,
                CourseTypeId = command.Model.CourseTypeId,
            };

            _courseRepository.Add(course);
        }
    }
}
