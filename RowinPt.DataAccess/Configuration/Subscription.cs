using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RowinPt.Domain;

namespace RowinPt.DataAccess.Configuration
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<SubscriptionModel>
    {
        public void Configure(EntityTypeBuilder<SubscriptionModel> builder)
        {
        }
    }
}
