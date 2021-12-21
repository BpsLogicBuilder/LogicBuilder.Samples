using Contoso.Data.Automatic;
using Contoso.Data.Rules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Contexts
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
