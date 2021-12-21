using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    interface ITableConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
