using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal class PersonalConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Personal>();
            entity.ToTable(nameof(Personal));
            entity.HasKey(p => p.UserId);
            entity.Property(p => p.FirstName).HasMaxLength(30);
            entity.Property(p => p.FirstName).IsRequired();
            entity.Property(p => p.MiddleName).HasMaxLength(20);
            entity.Property(p => p.LastName).HasMaxLength(30);
            entity.Property(p => p.LastName).IsRequired();
            entity.Property(p => p.Suffix).HasMaxLength(6);
            entity.Property(p => p.Address1).HasMaxLength(30);
            entity.Property(p => p.Address1).IsRequired();
            entity.Property(p => p.Address2).HasMaxLength(30);
            entity.Property(p => p.City).HasMaxLength(30);
            entity.Property(p => p.City).IsRequired();
            entity.Property(p => p.County).HasMaxLength(25);
            entity.Property(p => p.ZipCode).HasMaxLength(5);
            entity.Property(p => p.ZipCode).IsRequired();
            entity.Property(p => p.CellPhone).HasMaxLength(15);
            entity.Property(p => p.OtherPhone).HasMaxLength(15);
            entity.Property(p => p.PrimaryEmail).HasMaxLength(40);
        }
    }
}
