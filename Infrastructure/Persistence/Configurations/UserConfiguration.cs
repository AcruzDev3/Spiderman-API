using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder) {

            builder.HasKey(e => e.UserId).HasName("PK__user__B9BE370F1F9F253B");

            builder.ToTable("user");

            builder.HasIndex(e => e.Name, "UQ__user__72E12F1BDE6F6AE2").IsUnique();

            builder.HasIndex(e => e.Email, "UQ__user__AB6E6164872AA517").IsUnique();

            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            builder.Property(e => e.Image)
                .HasMaxLength(300)
                .HasColumnName("image");
            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            builder.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            builder.Property(e => e.RoleId).HasColumnName("role_id");

            builder.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user__role_id__056ECC6A");
        }
    }
}
