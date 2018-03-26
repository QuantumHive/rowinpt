using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.CommandHandlers.Locations
{
    internal sealed class CreateLocationCommandHandler : ICommandHandler<CreateCommand<Location>>
    {
        private readonly IRepository<LocationModel> _locationRepository;

        public CreateLocationCommandHandler(
            IRepository<LocationModel> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public void Handle(CreateCommand<Location> command)
        {
            var location = new LocationModel
            {
                Id = command.Model.Id,
                Name = command.Model.Name,
                Address = command.Model.Address,
            };

            _locationRepository.Add(location);
        }
    }
}
