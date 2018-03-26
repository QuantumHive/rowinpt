using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;


namespace RowinPt.Business.QueryHandlers.Locations
{
    internal sealed class GetAllLocationsQueryHandler : IQueryHandler<GetAllQuery<Location>, IEnumerable<Location>>
    {
        private readonly IReader<LocationModel> _locationReader;

        public GetAllLocationsQueryHandler(
            IReader<LocationModel> locationReader)
        {
            _locationReader = locationReader;
        }

        public IEnumerable<Location> Handle(GetAllQuery<Location> query)
        {
            return _locationReader.Entities.Select(location => new Location
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
            });
        }
    }
}
