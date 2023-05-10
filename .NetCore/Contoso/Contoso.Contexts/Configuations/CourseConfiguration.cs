using Contoso.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contoso.Contexts.Configuations
{
    internal class CourseConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Course>();
            entity.ToTable(nameof(Course));
            entity.HasKey(c => c.CourseID);
            entity.HasOne(c => c.Department).WithMany(d => d.Courses).HasForeignKey(c => c.DepartmentID);
            entity.HasMany(c => c.Enrollments).WithOne(e => e.Course).HasForeignKey(e => e.CourseID);
            entity.HasMany(c => c.Assignments).WithOne(ca => ca.Course).HasForeignKey(ca => ca.CourseID);
            entity.Property(c => c.Title).HasMaxLength(50);
        }
    }
}
