using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal class UserConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<User>();
            entity.ToTable(nameof(User));
            entity.HasKey(u => u.UserId);
            entity.HasOne(u => u.Personal).WithOne(p => p.User).HasForeignKey<Personal>(p => p.UserId);
            entity.HasOne(u => u.Academic).WithOne(a => a.User).HasForeignKey<Academic>(a => a.UserId);
            entity.HasOne(u => u.Admissions).WithOne(a => a.User).HasForeignKey<Admissions>(a => a.UserId);
            entity.HasOne(u => u.Certification).WithOne(c => c.User).HasForeignKey<Certification>(c => c.UserId);
            entity.HasOne(u => u.ContactInfo).WithOne(c => c.User).HasForeignKey<ContactInfo>(c => c.UserId);
            entity.HasOne(u => u.MoreInfo).WithOne(m => m.User).HasForeignKey<MoreInfo>(m => m.UserId);
            entity.HasOne(u => u.Residency).WithOne(r => r.User).HasForeignKey<Residency>(r => r.UserId);
        }
    }
}
