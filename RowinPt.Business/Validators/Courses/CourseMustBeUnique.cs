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
    internal sealed class CourseMustBeUnique : IValidator<CreateCommand<Course>>, IValidator<UpdateCommand<Course>>
    {
        private readonly IReader<CourseModel> _courseReader;

        public CourseMustBeUnique(IReader<CourseModel> courseReader)
        {
            _courseReader = courseReader;
        }

        public IEnumerable<ValidationObject> Validate(CreateCommand<Course> instance)
        {
            var course = _courseReader.Entities.SingleOrDefault(c =>
                c.Name.Equals(instance.Model.Name, StringComparison.InvariantCultureIgnoreCase));

            if(course != null)
            {
                yield return new ValidationObject
                {
                    Message = $"Een les met de naam '{instance.Model.Name}' bestaat al"
                };
            }
        }

        public IEnumerable<ValidationObject> Validate(UpdateCommand<Course> instance)
        {
            var course = _courseReader.Entities.SingleOrDefault(c =>
                c.Id != instance.Model.Id &&
                c.Name.Equals(instance.Model.Name, StringComparison.InvariantCultureIgnoreCase));

            if (course != null)
            {
                yield return new ValidationObject
                {
                    Message = $"Een les met de naam '{instance.Model.Name}' bestaat al"
                };
            }
        }
    }
}
