using Microsoft.EntityFrameworkCore;

namespace Contoso.Contexts
{
    public abstract class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
            this.EntityConfigurationHandler = new EntityConfigurationHandler(this);
        }

        protected virtual EntityConfigurationHandler EntityConfigurationHandler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            BaseDbContextSqlFunctions.Register(modelBuilder);
            this.EntityConfigurationHandler.Configure(modelBuilder);
        }
    }
}
