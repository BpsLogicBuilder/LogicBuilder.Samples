using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal class ResidencyConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Residency>();
            entity.ToTable(nameof(Residency));
            entity.HasKey(r => r.UserId);
            entity.HasMany(r => r.StatesLivedIn).WithOne(s => s.Residency).HasForeignKey(s => s.UserId);
            entity.Property(r => r.CitizenshipStatus).HasMaxLength(6);
            entity.Property(r => r.CitizenshipStatus).IsRequired();
            entity.Property(r => r.ImmigrationStatus).HasMaxLength(6);
            entity.Property(r => r.CountryOfCitizenship).HasMaxLength(55);
            entity.Property(r => r.ResidentState).HasMaxLength(55);
            entity.Property(r => r.CitizenshipStatus).IsRequired();
            entity.Property(r => r.HasValidDriversLicense).IsRequired();
            entity.Property(r => r.DriversLicenseState).HasMaxLength(10);
            entity.Property(r => r.DriversLicenseNumber).HasMaxLength(25);
        }
    }
}
