using Contoso.Contexts;
using Contoso.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MigrationTool
{
    public class MigrationContext : DbContext
    {
        public MigrationContext(DbContextOptions<MigrationContext> options) : base(options)
        {
            this.EntityConfigurationHandler = new EntityConfigurationHandler(this);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        public DbSet<LookUps> LookUps { get; set; }

        protected virtual EntityConfigurationHandler EntityConfigurationHandler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.EntityConfigurationHandler.Configure(modelBuilder);
        }
    }
}
