using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries.Profile;
using System.Linq;
using System.Collections.Generic;
using RowinPt.Domain;

namespace RowinPt.Business.QueryHandlers.Profile
{
    internal sealed class GetCustomerProfileQueryHandler : IQueryHandler<GetCustomerProfileQuery, IEnumerable<Measurement>>
    {
        private readonly IReader<MeasurementModel> _measurementReader;
        private readonly ITimeProvider _timeProvider;
        public GetCustomerProfileQueryHandler(
            IReader<MeasurementModel> measurementReader,
            ITimeProvider timeProvider)
        {
            _measurementReader = measurementReader;
            _timeProvider = timeProvider;
        }

        public IEnumerable<Measurement> Handle(GetCustomerProfileQuery query)
        {
            var minimum = _timeProvider.Today.StartOfMonth().AddMonths(-5);

            return
                from measurement in _measurementReader.Entities
                where measurement.CustomerId == query.UserId
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
