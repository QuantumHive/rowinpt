using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RowinPt.Domain;

namespace RowinPt.DataAccess.Configuration
{
    public class CourseTypeConfiguration : IEntityTypeConfiguration<CourseTypeModel>
    {
        public void Configure(EntityTypeBuilder<CourseTypeModel> builder)
        {
            builder.Property(nameof(CourseTypeModel.Name)).IsRequired();
        }
    }
}
