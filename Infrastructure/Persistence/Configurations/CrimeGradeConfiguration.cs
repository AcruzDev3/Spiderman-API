using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class CrimeGradeConfiguration : IEntityTypeConfiguration<CrimeGradeEntity>
    {
        public void Configure(EntityTypeBuilder<CrimeGradeEntity> builder) {
            builder.HasKey(e => e.CrimeGradeId).HasName("PK__crime_gr__19D16CCFE2B24E11");

            builder.ToTable("crime_grade");

            builder.HasIndex(e => e.Name, "UQ__crime_gr__72E12F1BF1903D8A").IsUnique();

            builder.Property(e => e.CrimeGradeId).HasColumnName("crime_grade_id");
            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        }
    }
}
