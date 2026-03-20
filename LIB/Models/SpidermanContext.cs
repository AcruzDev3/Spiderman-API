using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LIB.Models;

public partial class SpidermanContext : DbContext
{
    public SpidermanContext()
    {
    }

    public SpidermanContext(DbContextOptions<SpidermanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Crime> Crimes { get; set; }

    public virtual DbSet<CrimeGrade> CrimeGrades { get; set; }

    public virtual DbSet<CrimeType> CrimeTypes { get; set; }

    public virtual DbSet<Criminal> Criminals { get; set; }

    public virtual DbSet<CriminalRiskLevel> CriminalRiskLevels { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__address__CAA247C89A69E407");

            entity.ToTable("address");

            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Image)
                .HasMaxLength(150)
                .HasColumnName("image");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Side)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("side");
            entity.Property(e => e.Street)
                .HasMaxLength(150)
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<Crime>(entity =>
        {
            entity.HasKey(e => e.CrimeId).HasName("PK__crime__C10AEBBD87DE4A84");

            entity.ToTable("crime");

            entity.Property(e => e.CrimeId).HasColumnName("crime_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.DateEnd)
                .HasColumnType("datetime")
                .HasColumnName("date_end");
            entity.Property(e => e.DateStart)
                .HasColumnType("datetime")
                .HasColumnName("date_start");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.GradeId).HasColumnName("grade_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Address).WithMany(p => p.Crimes)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__crime__address_i__00AA174D");

            entity.HasOne(d => d.Grade).WithMany(p => p.Crimes)
                .HasForeignKey(d => d.GradeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__crime__grade_id__7EC1CEDB");

            entity.HasOne(d => d.Type).WithMany(p => p.Crimes)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__crime__type_id__7FB5F314");

            entity.HasMany(d => d.Criminals).WithMany(p => p.Crimes)
                .UsingEntity<Dictionary<string, object>>(
                    "CrimeCriminal",
                    r => r.HasOne<Criminal>().WithMany()
                        .HasForeignKey("CriminalId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__crime_cri__crimi__0D0FEE32"),
                    l => l.HasOne<Crime>().WithMany()
                        .HasForeignKey("CrimeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__crime_cri__crime__0C1BC9F9"),
                    j =>
                    {
                        j.HasKey("CrimeId", "CriminalId").HasName("PK__crime_cr__DB233D9C7CAAADFD");
                        j.ToTable("crime_criminals");
                        j.IndexerProperty<int>("CrimeId").HasColumnName("crime_id");
                        j.IndexerProperty<int>("CriminalId").HasColumnName("criminal_id");
                    });

            entity.HasMany(d => d.Heroes).WithMany(p => p.Crimes)
                .UsingEntity<Dictionary<string, object>>(
                    "CrimeHero",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__crime_her__hero___093F5D4E"),
                    l => l.HasOne<Crime>().WithMany()
                        .HasForeignKey("CrimeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__crime_her__crime__084B3915"),
                    j =>
                    {
                        j.HasKey("CrimeId", "HeroId").HasName("PK__crime_he__E952DC2ED24C64A6");
                        j.ToTable("crime_heros");
                        j.IndexerProperty<int>("CrimeId").HasColumnName("crime_id");
                        j.IndexerProperty<int>("HeroId").HasColumnName("hero_id");
                    });
        });

        modelBuilder.Entity<CrimeGrade>(entity =>
        {
            entity.HasKey(e => e.CrimeGradeId).HasName("PK__crime_gr__19D16CCFE2B24E11");

            entity.ToTable("crime_grade");

            entity.HasIndex(e => e.Name, "UQ__crime_gr__72E12F1BF1903D8A").IsUnique();

            entity.Property(e => e.CrimeGradeId).HasColumnName("crime_grade_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CrimeType>(entity =>
        {
            entity.HasKey(e => e.CrimeTypeId).HasName("PK__crime_ty__0290B785CD5B3E26");

            entity.ToTable("crime_type");

            entity.HasIndex(e => e.Name, "UQ__crime_ty__72E12F1B4AA98BA9").IsUnique();

            entity.Property(e => e.CrimeTypeId).HasColumnName("crime_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Criminal>(entity =>
        {
            entity.HasKey(e => e.CriminalId).HasName("PK__criminal__A29D62103CD52AD0");

            entity.ToTable("criminal");

            entity.Property(e => e.CriminalId).HasColumnName("criminal_id");
            entity.Property(e => e.CriminalSince)
                .HasColumnType("datetime")
                .HasColumnName("criminal_since");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.RiskId).HasColumnName("risk_id");

            entity.HasOne(d => d.Risk).WithMany(p => p.Criminals)
                .HasForeignKey(d => d.RiskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__criminal__risk_i__7BE56230");
        });

        modelBuilder.Entity<CriminalRiskLevel>(entity =>
        {
            entity.HasKey(e => e.CriminalRiskLevelId).HasName("PK__criminal__15FF8BAFFB9A156A");

            entity.ToTable("criminal_risk_level");

            entity.HasIndex(e => e.Name, "UQ__criminal__72E12F1BBEDE9AB5").IsUnique();

            entity.Property(e => e.CriminalRiskLevelId).HasColumnName("criminal_risk_level_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__role__760965CCDD36EF58");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "UQ__role__72E12F1B623E1EDD").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user__B9BE370F1F9F253B");

            entity.ToTable("user");

            entity.HasIndex(e => e.Name, "UQ__user__72E12F1BDE6F6AE2").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__user__AB6E6164872AA517").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Image)
                .HasMaxLength(300)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user__role_id__056ECC6A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
