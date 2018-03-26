using AlperAslanApps.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RowinPt.Domain;

namespace RowinPt.DataAccess.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("Users");
            builder.Property(nameof(UserModel.Name)).IsRequired();
            builder.Property(nameof(UserModel.Email)).IsRequired();
            builder.Property(nameof(UserModel.PhoneNumber)).IsRequired();
            builder.Property(nameof(UserModel.NormalizedEmail)).IsRequired();

            builder.Property(nameof(IModel.CreatedBy)).IsRequired();
            builder.Property(nameof(IModel.EditedBy)).IsRequired();
        }
    }
}
