using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CrimeConfiguration : IEntityTypeConfiguration<CrimeEntity>
    {
        public void Configure(EntityTypeBuilder<CrimeEntity> builder) {
            builder.HasKey(e => e.CrimeId).HasName("PK__crime__C10AEBBD87DE4A84");

            builder.ToTable("crime");

            builder.Property(e => e.CrimeId).HasColumnName("crime_id");
            builder.Property(e => e.AddressId).HasColumnName("address_id");
            builder.Property(e => e.DateEnd)
                .HasColumnType("datetime")
                .HasColumnName("date_end");
            builder.Property(e => e.DateStart)
                .HasColumnType("datetime")
                .HasColumnName("date_start");
            builder.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            builder.Property(e => e.GradeId).HasColumnName("grade_id");
            builder.Property(e => e.Status).HasColumnName("status");
            builder.Property(e => e.TypeId).HasColumnName("type_id");

            builder.HasOne(d => d.Address).WithMany(p => p.Crimes)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__crime__address_i__00AA174D");

            builder.HasOne(d => d.Grade).WithMany(p => p.Crimes)
                .HasForeignKey(d => d.GradeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__crime__grade_id__7EC1CEDB");

            builder.HasOne(d => d.Type).WithMany(p => p.Crimes)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__crime__type_id__7FB5F314");

            builder.HasMany(d => d.Criminals).WithMany(p => p.Crimes)
                .UsingEntity<Dictionary<string, object>>(
                    "CrimeCriminal",
                    r => r.HasOne<CriminalEntity>().WithMany()
                        .HasForeignKey("CriminalId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__crime_cri__crimi__0D0FEE32"),
                    l => l.HasOne<CrimeEntity>().WithMany()
                        .HasForeignKey("CrimeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__crime_cri__crime__0C1BC9F9"),
                    j => {
                        j.HasKey("CrimeId", "CriminalId").HasName("PK__crime_cr__DB233D9C7CAAADFD");
                        j.ToTable("crime_criminals");
                        j.IndexerProperty<int>("CrimeId").HasColumnName("crime_id");
                        j.IndexerProperty<int>("CriminalId").HasColumnName("criminal_id");
                    });

            builder.HasMany(d => d.Heroes).WithMany(p => p.Crimes)
                .UsingEntity<Dictionary<string, object>>(
                    "CrimeHero",
                    r => r.HasOne<UserEntity>().WithMany()
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__crime_her__hero___093F5D4E"),
                    l => l.HasOne<CrimeEntity>().WithMany()
                        .HasForeignKey("CrimeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__crime_her__crime__084B3915"),
                    j => {
                        j.HasKey("CrimeId", "HeroId").HasName("PK__crime_he__E952DC2ED24C64A6");
                        j.ToTable("crime_heros");
                        j.IndexerProperty<int>("CrimeId").HasColumnName("crime_id");
                        j.IndexerProperty<int>("HeroId").HasColumnName("hero_id");
                    });
        }
    }
}
