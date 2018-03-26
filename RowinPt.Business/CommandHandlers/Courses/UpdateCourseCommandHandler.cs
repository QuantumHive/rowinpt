using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.CommandHandlers.Courses
{
    internal sealed class UpdateCourseCommandHandler : ICommandHandler<UpdateCommand<Course>>
    {
        private readonly IReader<CourseModel> _courseReader;

        public UpdateCourseCommandHandler(IReader<CourseModel> courseReader)
        {
            _courseReader = courseReader;
        }

        public void Handle(UpdateCommand<Course> command)
        {
            var course = _courseReader.GetById(command.Model.Id);

            course.Name = command.Model.Name;
            course.Capacity = command.Model.Capacity;
            course.CourseTypeId = command.Model.CourseTypeId;
        }
    }
}
