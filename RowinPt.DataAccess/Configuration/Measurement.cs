using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RowinPt.Domain;

namespace RowinPt.DataAccess.Configuration
{
    public class MeasurementConfiguration : IEntityTypeConfiguration<MeasurementModel>
    {
        public void Configure(EntityTypeBuilder<MeasurementModel> builder)
        {
        }
    }
}
