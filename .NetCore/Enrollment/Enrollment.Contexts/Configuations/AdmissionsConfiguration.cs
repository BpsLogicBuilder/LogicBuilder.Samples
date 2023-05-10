using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal class AdmissionsConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Admissions>();
            entity.ToTable(nameof(Admissions));
            entity.HasKey(a => a.UserId);
            entity.Property(a => a.EnteringStatus).HasMaxLength(1);
            entity.Property(a => a.EnteringStatus).IsRequired();
            entity.Property(a => a.EnrollmentTerm).HasMaxLength(6);
            entity.Property(a => a.EnrollmentTerm).IsRequired();
            entity.Property(a => a.EnrollmentYear).HasMaxLength(4);
            entity.Property(a => a.EnrollmentYear).IsRequired();
            entity.Property(a => a.ProgramType).HasMaxLength(55);
            entity.Property(a => a.ProgramType).IsRequired();
            entity.Property(a => a.Program).HasMaxLength(55);
            entity.Property(a => a.Program).IsRequired();
        }
    }
}
