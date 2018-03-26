using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.CommandHandlers.Locations
{
    internal sealed class UpdateLocationCommandHandler : ICommandHandler<UpdateCommand<Location>>
    {
        private readonly IReader<LocationModel> _locationReader;

        public UpdateLocationCommandHandler(
            IReader<LocationModel> locationReader)
        {
            _locationReader = locationReader;
        }

        public void Handle(UpdateCommand<Location> command)
        {
            var location = _locationReader.GetById(command.Model.Id);

            location.Name = command.Model.Name;
            location.Address = command.Model.Address;
        }
    }
}
