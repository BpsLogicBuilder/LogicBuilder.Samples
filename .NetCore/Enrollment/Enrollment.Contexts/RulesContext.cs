using Enrollment.Data.Automatic;
using Enrollment.Data.Rules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Contexts
{
    public class RulesContext : BaseDbContext
    {
        public RulesContext(DbContextOptions<RulesContext> options) : base(options)
        {
        }

        public DbSet<RulesModule> RulesModule { get; set; }
        public DbSet<VariableMetaData> VariableMetaData { get; set; }
    }
}
