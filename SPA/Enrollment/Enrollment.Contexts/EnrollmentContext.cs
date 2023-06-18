using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts
{
    public class EnrollmentContext : BaseDbContext
    {
        public EnrollmentContext(DbContextOptions<EnrollmentContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
