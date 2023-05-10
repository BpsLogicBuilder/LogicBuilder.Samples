using Contoso.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contoso.Contexts.Configuations
{
    internal class OfficeAssignmentConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<OfficeAssignment>();
            entity.ToTable(nameof(OfficeAssignment));
            entity.HasKey(oa => oa.InstructorID);
            //modelBuilder.Entity<OfficeAssignment>().HasOne(oa => oa.Instructor).WithOne(i => i.OfficeAssignment).HasForeignKey<Instructor>(i => i.ID);
            //OfficeAssignment.InstructorID is the foreign key in the database so this will cause the exception
            //The value of 'Instructor.ID' is unknown when attempting to save changes. This is because the property is also part of a foreign key for which the principal entity in the relationship is not known.
            //This one-to-one configuration is handled by the parent class' configuration (InstructorConfiguration).

            entity.Property(i => i.Location).HasMaxLength(50);
        }
    }
}
