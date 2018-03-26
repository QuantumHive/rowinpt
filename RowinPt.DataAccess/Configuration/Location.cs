using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RowinPt.Domain;

namespace RowinPt.DataAccess.Configuration
{
    public class LocationConfiguration : IEntityTypeConfiguration<LocationModel>
    {
        public void Configure(EntityTypeBuilder<LocationModel> builder)
        {
            builder.Property(nameof(LocationModel.Name)).IsRequired();
            builder.Property(nameof(LocationModel.Address)).IsRequired();
        }
    }
}
