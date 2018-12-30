using Enrollment.Data.Rules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Contexts.Configuations
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
