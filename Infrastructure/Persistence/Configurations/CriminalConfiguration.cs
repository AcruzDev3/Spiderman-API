using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CriminalConfiguration : IEntityTypeConfiguration<CriminalEntity>
    {
        public void Configure(EntityTypeBuilder<CriminalEntity> builder)
        {
            builder.HasKey(e => e.CriminalId).HasName("PK__criminal__A29D62103CD52AD0");

            builder.ToTable("criminal");

            builder.Property(e => e.CriminalId).HasColumnName("criminal_id");
            builder.Property(e => e.CriminalSince)
                .HasColumnType("datetime")
                .HasColumnName("criminal_since");
            builder.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            builder.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            builder.Property(e => e.RiskId).HasColumnName("risk_id");

            builder.HasOne(d => d.Risk).WithMany(p => p.Criminals)
                .HasForeignKey(d => d.RiskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__criminal__risk_i__7BE56230");
        }
    }
}
