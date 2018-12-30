using Enrollment.Contexts;
using Enrollment.Data.Automatic;
using Enrollment.Data.Entities;
using Enrollment.Data.Rules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MigrationTool
{
    public class MigrationContext : DbContext
    {
        public MigrationContext()
        {
            this.EntityConfigurationHandler = new EntityConfigurationHandler(this);
        }

        public DbSet<Personal> Personal { get; set; }
        public DbSet<Academic> Academic { get; set; }
        public DbSet<Admissions> Admissions { get; set; }
        public DbSet<Certification> Certification { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<MoreInfo> MoreInfo { get; set; }
        public DbSet<Residency> Residency { get; set; }
        public DbSet<StateLivedIn> StatesLivedIn { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<LookUps> LookUps { get; set; }

        public DbSet<RulesModule> RulesModule { get; set; }
        public DbSet<VariableMetaData> VariableMetaData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {//Can't use DI to create MigrationContext for dotnet ef migrations add
            optionsBuilder.UseSqlServer(@"Server=.\SQL2014;Database=Enrollment;Trusted_Connection=True;");
            //Alternatively use DI and at runtime use
            //myDbContext.Database.Migrate(); Then context.Database.EnsureCreated();
            //Instead of "dotnet ef database update -v" at the command line.
        }

        protected virtual EntityConfigurationHandler EntityConfigurationHandler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.EntityConfigurationHandler.Configure(modelBuilder);
        }
    }
}
