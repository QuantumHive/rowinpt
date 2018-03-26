using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RowinPt.Domain;

namespace RowinPt.DataAccess.Configuration
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<ScheduleModel>
    {
        public void Configure(EntityTypeBuilder<ScheduleModel> builder)
        {
            builder.Property(nameof(ScheduleModel.Name)).IsRequired();
        }
    }
}
