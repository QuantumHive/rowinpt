using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.QueryHandlers.CourseTypes
{
    internal sealed class GetCourseTypeByIdQueryHandler : IQueryHandler<GetByIdQuery<CourseType>, CourseType>
    {
        private readonly IReader<CourseTypeModel> _courseTypeReader;

        public GetCourseTypeByIdQueryHandler(IReader<CourseTypeModel> courseTypeReader)
        {
            _courseTypeReader = courseTypeReader;
        }
        public CourseType Handle(GetByIdQuery<CourseType> query)
        {
            var courseType = _courseTypeReader.GetById(query.Id);

            return new CourseType
            {
                Id = courseType.Id,
                Name = courseType.Name,
                Capacity = courseType.Capacity,
            };
        }
    }
}
