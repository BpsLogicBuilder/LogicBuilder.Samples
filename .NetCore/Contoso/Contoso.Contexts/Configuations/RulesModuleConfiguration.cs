using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Contoso.Data.Rules;

namespace Contoso.Contexts.Configuations
{
    class RulesModuleConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RulesModule>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.Application })
                    .HasName("uc_RulesModule")
                    .IsUnique();
            });
        }
    }
}
