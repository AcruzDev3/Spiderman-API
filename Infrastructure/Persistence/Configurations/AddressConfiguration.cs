using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder) {
            builder.HasKey(e => e.AddressId).HasName("PK__address__CAA247C89A69E407");

            builder.ToTable("address");

            builder.Property(e => e.AddressId).HasColumnName("address_id");
            builder.Property(e => e.Image)
                .HasMaxLength(150)
                .HasColumnName("image");
            builder.Property(e => e.Number).HasColumnName("number");
            builder.Property(e => e.Side)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("side")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            builder.Property(e => e.Street)
                .HasMaxLength(150)
                .HasColumnName("street")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            builder.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("zip_code");
        }
    }
}
