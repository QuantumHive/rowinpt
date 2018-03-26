using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.CommandHandlers.CourseTypes
{
    internal sealed class CreateCourseTypeCommandHandler : ICommandHandler<CreateCommand<CourseType>>
    {
        private readonly IRepository<CourseTypeModel> _courseTypeRepository;

        public CreateCourseTypeCommandHandler(IRepository<CourseTypeModel> courseTypeRepository)
        {
            _courseTypeRepository = courseTypeRepository;
        }

        public void Handle(CreateCommand<CourseType> command)
        {
            var courseType = new CourseTypeModel
            {
                Id = command.Model.Id,
                Name = command.Model.Name,
                Capacity = command.Model.Capacity,
            };

            _courseTypeRepository.Add(courseType);
        }
    }
}
