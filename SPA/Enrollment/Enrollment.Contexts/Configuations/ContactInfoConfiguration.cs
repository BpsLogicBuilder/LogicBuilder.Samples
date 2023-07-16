using Enrollment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Contexts.Configuations
{
    internal class ContactInfoConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ContactInfo>();
            entity.ToTable(nameof(ContactInfo));
            entity.HasKey(c => c.UserId);
            entity.Property(c => c.HasFormerName).IsRequired();
            entity.Property(c => c.FormerFirstName).HasMaxLength(30);
            entity.Property(c => c.FormerLastName).HasMaxLength(30);
            entity.Property(c => c.DateOfBirth).IsRequired();
            entity.Property(c => c.SocialSecurityNumber).IsRequired();
            entity.Property(c => c.SocialSecurityNumber).HasMaxLength(11);
            entity.Property(c => c.Gender).IsRequired();
            entity.Property(c => c.Gender).HasMaxLength(1);
            entity.Property(c => c.Race).IsRequired();
            entity.Property(c => c.Race).HasMaxLength(3);
            entity.Property(c => c.Ethnicity).IsRequired();
            entity.Property(c => c.Ethnicity).HasMaxLength(3);
            entity.Property(c => c.EnergencyContactFirstName).IsRequired();
            entity.Property(c => c.EnergencyContactFirstName).HasMaxLength(30);
            entity.Property(c => c.EnergencyContactLastName).IsRequired();
            entity.Property(c => c.EnergencyContactLastName).HasMaxLength(30);
            entity.Property(c => c.EnergencyContactRelationship).IsRequired();
            entity.Property(c => c.EnergencyContactRelationship).HasMaxLength(30);
            entity.Property(c => c.EnergencyContactPhoneNumber).IsRequired();
            entity.Property(c => c.EnergencyContactPhoneNumber).HasMaxLength(12);
        }
    }
}
