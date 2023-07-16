using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal class MoreInfoConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<MoreInfo>();
            entity.ToTable(nameof(MoreInfo));
            entity.HasKey(m => m.UserId);
            entity.Property(m => m.ReasonForAttending).HasMaxLength(4);
            entity.Property(m => m.ReasonForAttending).IsRequired();
            entity.Property(m => m.OverallEducationalGoal).HasMaxLength(4);
            entity.Property(m => m.OverallEducationalGoal).IsRequired();
            entity.Property(m => m.IsVeteran).IsRequired();
            entity.Property(m => m.MilitaryStatus).HasMaxLength(4);
            entity.Property(m => m.MilitaryBranch).HasMaxLength(4);
            entity.Property(m => m.VeteranType).HasMaxLength(4);
            entity.Property(m => m.GovernmentBenefits).HasMaxLength(10);
        }
    }
}
