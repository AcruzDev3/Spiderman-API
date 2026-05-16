using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF_Entities;

public partial class SpidermanContext : DbContext
{
    public SpidermanContext(DbContextOptions<SpidermanContext> options)
        : base(options) { }

    public virtual DbSet<AddressEntity> Addresses { get; set; }

    public virtual DbSet<CrimeEntity> Crimes { get; set; }

    public virtual DbSet<CrimeGradeEntity> CrimeGrades { get; set; }

    public virtual DbSet<CrimeTypeEntity> CrimeTypes { get; set; }

    public virtual DbSet<CriminalEntity> Criminals { get; set; }

    public virtual DbSet<CriminalRiskLevelEntity> CriminalRiskLevels { get; set; }

    public virtual DbSet<RoleEntity> Roles { get; set; }

    public virtual DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpidermanContext).Assembly);
    }
}
