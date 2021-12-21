using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts
{
    public class EnrollmentContext : BaseDbContext
    {
        public EnrollmentContext(DbContextOptions<EnrollmentContext> options) : base(options)
        {
        }

        public DbSet<Personal> Personal { get; set; }
        public DbSet<Academic> Academic { get; set; }
        public DbSet<Admissions> Admissions { get; set; }
        public DbSet<Certification> Certification { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<MoreInfo> MoreInfo { get; set; }
        public DbSet<Residency> Residency { get; set; }
        public DbSet<StateLivedIn> StatesLivedIn { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<LookUps> LookUps { get; set; }
    }
}
