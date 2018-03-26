using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.Courses
{
    internal sealed class GetAllCourseQueryHandler : IQueryHandler<GetAllQuery<Course>, IEnumerable<Course>>
    {
        private readonly IReader<CourseModel> _courseReader;

        public GetAllCourseQueryHandler(
            IReader<CourseModel> courseReader)
        {
            _courseReader = courseReader;
        }

        public IEnumerable<Course> Handle(GetAllQuery<Course> query)
        {
            return _courseReader.Entities.Select(courseType => new Course
            {
                Id = courseType.Id,
                Name = courseType.Name,
                Capacity = courseType.Capacity,
                CourseTypeId = courseType.CourseTypeId,
            });
        }
    }
}
