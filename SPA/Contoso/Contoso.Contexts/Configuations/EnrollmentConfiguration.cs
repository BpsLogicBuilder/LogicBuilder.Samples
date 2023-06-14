using Contoso.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contoso.Contexts.Configuations
{
    internal class EnrollmentConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Enrollment>();
            entity.ToTable(nameof(Enrollment));
            entity.HasKey(e => e.EnrollmentID);
            entity.HasOne(e => e.Course).WithMany(c => c.Enrollments).HasForeignKey(e => e.CourseID);
            entity.HasOne(e => e.Student).WithMany(c => c.Enrollments).HasForeignKey(e => e.StudentID);
        }
    }
}
