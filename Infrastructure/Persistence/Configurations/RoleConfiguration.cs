using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder) {
            builder.HasKey(e => e.RoleId).HasName("PK__role__760965CCDD36EF58");

            builder.ToTable("role");

            builder.HasIndex(e => e.Name, "UQ__role__72E12F1B623E1EDD").IsUnique();

            builder.Property(e => e.RoleId).HasColumnName("role_id");
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        }
    }
}
