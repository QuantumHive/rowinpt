using System;
using System.Collections.Generic;
using System.Linq;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using AlperAslanApps.Core.Models;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.Validators.Locations
{
    internal sealed class LocationMustByUnique : IValidator<CreateCommand<Location>>, IValidator<UpdateCommand<Location>>
    {
        private readonly IReader<LocationModel> _locationReader;

        public LocationMustByUnique(IReader<LocationModel> locationReader)
        {
            _locationReader = locationReader;
        }

        public IEnumerable<ValidationObject> Validate(CreateCommand<Location> instance)
        {
            var location = _locationReader.Entities.SingleOrDefault(pt =>
                pt.Name.Equals(instance.Model.Name, StringComparison.InvariantCultureIgnoreCase));

            if (location != null)
            {
                yield return new ValidationObject
                {
                    Message = $"Een locatie met de naam '{instance.Model.Name}' bestaat al"
                };
            }
        }

        public IEnumerable<ValidationObject> Validate(UpdateCommand<Location> instance)
        {
            var location = _locationReader.Entities.SingleOrDefault(pt =>
                pt.Id != instance.Model.Id &&
                pt.Name.Equals(instance.Model.Name, StringComparison.InvariantCultureIgnoreCase));

            if(location != null)
            {
                yield return new ValidationObject
                {
                    Message = $"Een locatie met de naam '{instance.Model.Name}' bestaat al"
                };
            }
        }
    }
}
