using Contoso.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contoso.Contexts.Configuations
{
    internal class LookUpsConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<LookUps>();
            entity.ToTable(nameof(LookUps));
            entity.HasKey(i => i.LookUpsID);
            entity.Property(i => i.Text).IsRequired();
            entity.Property(i => i.ListName).HasMaxLength(100);
            entity.Property(i => i.ListName).IsRequired();
            entity.Property(i => i.Value).HasMaxLength(256);
        }
    }
}
