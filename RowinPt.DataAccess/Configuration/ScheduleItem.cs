using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RowinPt.Domain;

namespace RowinPt.DataAccess.Configuration
{
    public class ScheduleItemConfiguration : IEntityTypeConfiguration<ScheduleItemModel>
    {
        public void Configure(EntityTypeBuilder<ScheduleItemModel> builder)
        {
        }
    }
}
