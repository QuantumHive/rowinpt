using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RowinPt.Domain;

namespace RowinPt.DataAccess.Configuration
{
    public class AbsenceNotesConfiguration : IEntityTypeConfiguration<AbsenceNotesModel>
    {
        public void Configure(EntityTypeBuilder<AbsenceNotesModel> builder)
        {
        }
    }
}
