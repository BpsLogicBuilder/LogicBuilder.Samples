using Enrollment.Contexts;
using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MigrationTool
{
    public class MigrationContext : DbContext
    {
        public MigrationContext(DbContextOptions<MigrationContext> options) : base(options)
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

        protected virtual EntityConfigurationHandler EntityConfigurationHandler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.EntityConfigurationHandler.Configure(modelBuilder);
        }
    }
}
