using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CriminalRiskLevelConfiguration : IEntityTypeConfiguration<CriminalRiskLevelEntity>
    {
        public void Configure(EntityTypeBuilder<CriminalRiskLevelEntity> builder) {
            builder.HasKey(e => e.CriminalRiskLevelId).HasName("PK__criminal__15FF8BAFFB9A156A");

            builder.ToTable("criminal_risk_level");

            builder.HasIndex(e => e.Name, "UQ__criminal__72E12F1BBEDE9AB5").IsUnique();

            builder.Property(e => e.CriminalRiskLevelId).HasColumnName("criminal_risk_level_id");
            builder.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        }
    }
}
