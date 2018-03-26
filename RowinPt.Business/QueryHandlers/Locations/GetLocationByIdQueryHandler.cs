using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.QueryHandlers.Locations
{
    internal sealed class GetLocationByIdQueryHandler : IQueryHandler<GetByIdQuery<Location>, Location>
    {
        private readonly IReader<LocationModel> _locationReader;

        public GetLocationByIdQueryHandler(
            IReader<LocationModel> locationReader)
        {
            _locationReader = locationReader;
        }

        public Location Handle(GetByIdQuery<Location> query)
        {
            var location = _locationReader.GetById(query.Id);

            return new Location
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
            };
        }
    }
}
