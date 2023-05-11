using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal class CertificationConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Certification>();
            entity.ToTable(nameof(Certification));
            entity.HasKey(a => a.UserId);
        }
    }
}
