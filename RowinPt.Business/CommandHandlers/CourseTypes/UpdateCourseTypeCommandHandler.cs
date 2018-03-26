using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.CommandHandlers.CourseTypes
{
    internal sealed class UpdateCourseTypeCommandHandler : ICommandHandler<UpdateCommand<CourseType>>
    {
        private readonly IReader<CourseTypeModel> _courseTypeReader;
        private readonly IReader<CourseModel> _courseReader;

        public UpdateCourseTypeCommandHandler(
            IReader<CourseTypeModel> courseTypeReader,
            IReader<CourseModel> courseReader)
        {
            _courseTypeReader = courseTypeReader;
            _courseReader = courseReader;
        }

        public void Handle(UpdateCommand<CourseType> command)
        {
            var courseType = _courseTypeReader.GetById(command.Model.Id);

            courseType.Name = command.Model.Name;
            courseType.Capacity = command.Model.Capacity;

            var courseIds =
                from course in _courseReader.Entities
                where course.CourseTypeId == command.Model.Id
                select course;

            foreach (var course in courseIds)
            {
                course.Capacity = command.Model.Capacity;
            }
        }
    }
}
