using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RowinPt.Domain;

namespace RowinPt.DataAccess.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<CourseModel>
    {
        public void Configure(EntityTypeBuilder<CourseModel> builder)
        {
            builder.Property(nameof(CourseModel.Name)).IsRequired();
        }
    }
}
