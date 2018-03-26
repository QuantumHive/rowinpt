using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.CourseTypes
{
    internal sealed class GetAllCourseTypesQueryHandler : IQueryHandler<GetAllQuery<CourseType>, IEnumerable<CourseType>>
    {
        private readonly IReader<CourseTypeModel> _courseTypeReader;

        public GetAllCourseTypesQueryHandler(
            IReader<CourseTypeModel> courseTypeReader)
        {
            _courseTypeReader = courseTypeReader;
        }

        public IEnumerable<CourseType> Handle(GetAllQuery<CourseType> query)
        {
            return _courseTypeReader.Entities.Select(courseType => new CourseType
            {
                Id = courseType.Id,
                Name = courseType.Name,
                Capacity = courseType.Capacity,
            });
        }
    }
}
