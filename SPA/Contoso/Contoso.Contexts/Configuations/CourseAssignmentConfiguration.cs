using Contoso.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contoso.Contexts.Configuations
{
    class CourseAssignmentConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<CourseAssignment>();
            entity.ToTable(nameof(CourseAssignment));
            entity.HasKey(c => new { c.CourseID, c.InstructorID });
            entity.HasOne(ca => ca.Instructor).WithMany(i => i.Courses).HasForeignKey(ca => ca.InstructorID);
            entity.HasOne(ca => ca.Course).WithMany(c => c.Assignments).HasForeignKey(ca => ca.CourseID);
        }
    }
}
