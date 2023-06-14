using Contoso.Contexts;
using Contoso.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MigrationTool
{
    public class MigrationContext : DbContext
    {
        public MigrationContext()
        {
            this.EntityConfigurationHandler = new EntityConfigurationHandler(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {//Can't use DI to create MigrationContext for dotnet ef migrations add
            optionsBuilder.UseSqlServer(@"Server=.\SQL2016;Database=Contoso;Trusted_Connection=True;trustServerCertificate=true;");
            //Alternatively use DI and at runtime use
            //myDbContext.Database.Migrate(); Then context.Database.EnsureCreated();
            //Instead of "dotnet ef database update -v" at the command line.
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
