using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RowinPt.Domain;

namespace RowinPt.DataAccess.Configuration
{
    public class AgendaConfiguration : IEntityTypeConfiguration<AgendaModel>
    {
        public void Configure(EntityTypeBuilder<AgendaModel> builder)
        {
        }
    }
}
