using Contoso.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contoso.Contexts.Configuations
{
    internal class StudentConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Student>();
            entity.ToTable(nameof(Student));
            entity.HasKey(s => s.ID);
            entity.HasMany(s => s.Enrollments).WithOne(ca => ca.Student).HasForeignKey(s => s.StudentID);
            entity.Property(s => s.FirstName).IsRequired();
            entity.Property(s => s.FirstName).HasMaxLength(50);
            entity.Property(s => s.LastName).IsRequired();
            entity.Property(s => s.LastName).HasMaxLength(50);
        }
    }
}
