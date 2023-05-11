using Enrollment.Data.Automatic;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal class VariableMetaDataConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<VariableMetaData>();
            entity.ToTable(nameof(VariableMetaData), "Automatic");
            entity.HasKey(i => i.VariableMetaDataId);
        }
    }
}
