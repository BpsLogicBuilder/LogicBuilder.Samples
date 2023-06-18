using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal class PersonConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Person>();
            entity.ToTable(nameof(Person));
            entity.HasKey(s => s.ID);
            entity.Property(s => s.FirstName).IsRequired();
            entity.Property(s => s.FirstName).HasMaxLength(50);
            entity.Property(s => s.LastName).IsRequired();
            entity.Property(s => s.LastName).HasMaxLength(50);
        }
    }
}
