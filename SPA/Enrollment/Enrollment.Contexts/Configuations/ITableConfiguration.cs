using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal interface ITableConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
