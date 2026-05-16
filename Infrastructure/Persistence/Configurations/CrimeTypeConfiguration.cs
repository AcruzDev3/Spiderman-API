using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CrimeTypeConfiguration : IEntityTypeConfiguration<CrimeTypeEntity>
    {
        public void Configure(EntityTypeBuilder<CrimeTypeEntity> builder) {
            builder.HasKey(e => e.CrimeTypeId).HasName("PK__crime_ty__0290B785CD5B3E26");

            builder.ToTable("crime_type");

            builder.HasIndex(e => e.Name, "UQ__crime_ty__72E12F1B4AA98BA9").IsUnique();

            builder.Property(e => e.CrimeTypeId).HasColumnName("crime_type_id");
            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        }
    }
}
