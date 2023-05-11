using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal class AcademicConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Academic>();
            entity.ToTable(nameof(Academic));
            entity.HasKey(a => a.UserId);
            entity.HasMany(a => a.Institutions).WithOne(i => i.Academic).HasForeignKey(i => i.UserId);
            entity.Property(a => a.LastHighSchoolLocation).HasMaxLength(256);
            entity.Property(a => a.LastHighSchoolLocation).IsRequired();
            entity.Property(a => a.NcHighSchoolName).HasMaxLength(256);
            entity.Property(a => a.HomeSchoolType).HasMaxLength(256);
            entity.Property(a => a.HomeSchoolAssociation).HasMaxLength(256);
            entity.Property(a => a.FromDate).IsRequired();
            entity.Property(a => a.ToDate).IsRequired();
            entity.Property(a => a.GraduationStatus).HasMaxLength(256);
            entity.Property(a => a.GraduationStatus).IsRequired();
            entity.Property(a => a.EarnedCreditAtCmc).IsRequired();

        }
    }
}
