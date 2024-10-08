﻿using Contoso.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contoso.Contexts.Configuations
{
    class DepartmentConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Department>();
            entity.ToTable(nameof(Department));
            entity.HasKey(d => d.DepartmentID);
            entity
                .HasOne(d => d.Administrator)
                .WithMany()
                .HasForeignKey(k => k.InstructorID)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasMany(d => d.Courses).WithOne(c => c.Department).HasForeignKey(c => c.DepartmentID);
            entity.Property(d => d.Name).HasMaxLength(50);
            entity.Property(d => d.Budget).HasColumnType("money");
            entity.Property(d => d.RowVersion).IsRowVersion();
        }
    }
}
