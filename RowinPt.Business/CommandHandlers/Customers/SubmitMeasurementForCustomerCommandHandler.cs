using AlperAslanApps.Core;
using RowinPt.Contract.Commands.Customers;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System;

namespace RowinPt.Business.CommandHandlers.Customers
{
    internal sealed class SubmitMeasurementForCustomerCommandHandler : ICommandHandler<SubmitMeasurementForCustomerCommand>
    {
        private readonly IReader<MeasurementModel> _measurementReader;
        private readonly IRepository<MeasurementModel> _measurementRepository;

        public SubmitMeasurementForCustomerCommandHandler(
            IReader<MeasurementModel> measurementReader,
            IRepository<MeasurementModel> measurementRepository)
        {
            _measurementReader = measurementReader;
            _measurementRepository = measurementRepository;
        }

        public void Handle(SubmitMeasurementForCustomerCommand command)
        {
            if(command.Measurement.Id == Guid.Empty)
            {
                var measurement = new MeasurementModel
                {
                    CustomerId = command.CustomerId,
                    Date = command.Measurement.Date,
                };

                Map(measurement, command.Measurement);
                _measurementRepository.Add(measurement);
            }
            else
            {
                var measurement = _measurementReader.GetById(command.Measurement.Id);
                Map(measurement, command.Measurement);
            }
        }

        private void Map(MeasurementModel destination, Measurement source)
        {
            destination.Weight = source.Weight;
            destination.FatPercentage = source.FatPercentage;
            destination.ShoulderSize = source.ShoulderSize;
            destination.ArmSize = source.ArmSize;
            destination.BellySize = source.BellySize;
            destination.WaistSize = source.WaistSize;
            destination.UpperLegSize = source.UpperLegSize;
        }
    }
}
