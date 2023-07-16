using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal class StateLivedInConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<StateLivedIn>();
            entity.ToTable("StatesLivedIn");
            entity.HasKey(s => s.StateLivedInId);
            entity.HasOne(s => s.Residency).WithMany(r => r.StatesLivedIn).HasForeignKey(s => s.UserId);
            entity.Property(s => s.State).HasMaxLength(25);
            entity.Property(s => s.State).IsRequired();
        }
    }
}
