using Enrollment.Data.Rules;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    class RulesModuleConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<RulesModule>();
            entity.ToTable(nameof(RulesModule), "Rules");
            entity.HasIndex(e => new { e.Name, e.Application }).HasDatabaseName("uc_RulesModule").IsUnique();
            entity.HasKey(r => r.RulesModuleId);
            entity.Property(r => r.Name).IsRequired();
            entity.Property(r => r.Name).HasColumnType("varchar(100)");
            entity.Property(r => r.Application).IsRequired();
            entity.Property(r => r.Application).HasColumnType("varchar(100)");
            entity.Property(r => r.RuleSetFile).IsRequired();
            entity.Property(r => r.ResourceSetFile).IsRequired();
            entity.Property(r => r.LoggedInUserId).HasColumnType("varchar(100)");
        }
    }
}
