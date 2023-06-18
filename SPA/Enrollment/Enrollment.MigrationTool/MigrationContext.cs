using Enrollment.Contexts;
using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.MigrationTool
{
    public class MigrationContext : DbContext
    {
        public MigrationContext()
        {
            this.EntityConfigurationHandler = new EntityConfigurationHandler(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {//Can't use DI to create MigrationContext for dotnet ef migrations add
            optionsBuilder.UseSqlServer(@"Server=.\SQL2016;Database=Enrollment;Trusted_Connection=True;trustServerCertificate=true;");
            //Alternatively use DI and at runtime use
            //myDbContext.Database.Migrate(); Then context.Database.EnsureCreated();
            //Instead of "dotnet ef database update -v" at the command line.
        }

        public DbSet<Person> Persons { get; set; }

        protected virtual EntityConfigurationHandler EntityConfigurationHandler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.EntityConfigurationHandler.Configure(modelBuilder);
        }
    }
}
