using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.QueryHandlers.CourseTypes
{
    internal sealed class GetCourseByIdQueryHandler : IQueryHandler<GetByIdQuery<Course>, Course>
    {
        private readonly IReader<CourseModel> _courseReader;

        public GetCourseByIdQueryHandler(IReader<CourseModel> courseReader)
        {
            _courseReader = courseReader;
        }
        public Course Handle(GetByIdQuery<Course> query)
        {
            var course = _courseReader.GetById(query.Id);

            return new Course
            {
                Id = course.Id,
                Name = course.Name,
                Capacity = course.Capacity,
                CourseTypeId = course.CourseTypeId,
            };
        }
    }
}
