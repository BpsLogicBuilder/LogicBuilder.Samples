using Contoso.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contoso.Contexts.Configuations
{
    internal class InstructorConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Instructor>();
            entity.ToTable(nameof(Instructor));
            entity.HasKey(i => i.ID);
            entity.HasMany(i => i.Courses).WithOne(ca => ca.Instructor).HasForeignKey(ca => ca.InstructorID);
            entity.HasOne(i => i.OfficeAssignment).WithOne(oa => oa.Instructor).HasForeignKey<OfficeAssignment>(oa => oa.InstructorID);
            entity.Property(d => d.LastName).HasMaxLength(50);
            entity.Property(d => d.LastName).IsRequired();
            entity.Property(d => d.FirstName).HasMaxLength(50);
            entity.Property(d => d.FirstName).IsRequired();
        }
    }
}
