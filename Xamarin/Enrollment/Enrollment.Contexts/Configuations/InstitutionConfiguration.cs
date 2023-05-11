using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal class InstitutionConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Institution>();
            entity.ToTable("Institutions");
            entity.HasKey(i => i.InstitutionId);
            entity.HasOne(i => i.Academic).WithMany(a => a.Institutions).HasForeignKey(i => i.UserId);
            entity.Property(i => i.InstitutionState).HasMaxLength(256);
            entity.Property(i => i.InstitutionState).IsRequired();
            entity.Property(i => i.InstitutionName).IsRequired();
            entity.Property(i => i.StartYear).HasMaxLength(4);
            entity.Property(i => i.EndYear).HasMaxLength(4);
            entity.Property(i => i.HighestDegreeEarned).HasMaxLength(4);
        }
    }
}
