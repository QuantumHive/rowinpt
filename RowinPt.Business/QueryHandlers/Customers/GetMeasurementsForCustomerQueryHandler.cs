using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries.Customers;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.Customers
{
    internal sealed class GetMeasurementsForCustomerQueryHandler
        : IQueryHandler<GetMeasurementsForCustomerQuery, IEnumerable<Measurement>>
    {
        private readonly IReader<MeasurementModel> _measurementReader;
        private readonly ITimeProvider _timeProvider;

        public GetMeasurementsForCustomerQueryHandler(
            IReader<MeasurementModel> measurementReader,
            ITimeProvider timeProvider)
        {
            _measurementReader = measurementReader;
            _timeProvider = timeProvider;
        }

        public IEnumerable<Measurement> Handle(GetMeasurementsForCustomerQuery query)
        {
            var minimum = _timeProvider.Today.StartOfMonth().AddMonths(-12);

            return
                from measurement in _measurementReader.Entities
                where measurement.CustomerId == query.CustomerId
                where measurement.Date >= minimum
                select new Measurement
                {
                    Id = measurement.Id,
                    Date = measurement.Date,
                    Weight = measurement.Weight,
                    FatPercentage = measurement.FatPercentage,
                    ArmSize = measurement.ArmSize,
                    BellySize = measurement.BellySize,
                    ShoulderSize = measurement.ShoulderSize,
                    UpperLegSize = measurement.UpperLegSize,
                    WaistSize = measurement.WaistSize,
                };
        }
    }
}
