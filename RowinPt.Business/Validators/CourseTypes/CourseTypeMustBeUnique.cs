using System;
using System.Collections.Generic;
using System.Linq;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using AlperAslanApps.Core.Models;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.Validators.CourseTypes
{
    internal sealed class CourseTypeMustBeUnique : IValidator<CreateCommand<CourseType>>, IValidator<UpdateCommand<CourseType>>
    {
        private readonly IReader<CourseTypeModel> _courseTypeReader;

        public CourseTypeMustBeUnique(IReader<CourseTypeModel> courseTypeReader)
        {
            _courseTypeReader = courseTypeReader;
        }

        public IEnumerable<ValidationObject> Validate(CreateCommand<CourseType> instance)
        {
            var courseType = _courseTypeReader.Entities.SingleOrDefault(c =>
                c.Name.Equals(instance.Model.Name, StringComparison.InvariantCultureIgnoreCase));

            if(courseType != null)
            {
                yield return new ValidationObject
                {
                    Message = $"Een lesvorm met de naam '{instance.Model.Name}' bestaat al"
                };
            }
        }

        public IEnumerable<ValidationObject> Validate(UpdateCommand<CourseType> instance)
        {
            var courseType = _courseTypeReader.Entities.SingleOrDefault(c =>
                c.Id != instance.Model.Id &&
                c.Name.Equals(instance.Model.Name, StringComparison.InvariantCultureIgnoreCase));

            if (courseType != null)
            {
                yield return new ValidationObject
                {
                    Message = $"Een lesvorm met de naam '{instance.Model.Name}' bestaat al"
                };
            }
        }
    }
}
