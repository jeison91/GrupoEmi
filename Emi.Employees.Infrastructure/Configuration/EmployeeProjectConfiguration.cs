using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emi.Employees.Domain.Entities;

namespace Emi.Employees.Infrastructure.Configuration
{
    public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProjectEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeProjectEntity> builder)
        {
            builder.ToTable("EmployeeProjects");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProjectId).IsRequired();
            builder.Property(x => x.EmployeeId).IsRequired();

            builder.HasOne(x => x.EmployeeTrace)
              .WithMany(x => x.EmployeeProjectTrace)
              .HasForeignKey(x => x.EmployeeId)
              .HasConstraintName("FK_EmployeeProject_Employee")
              .OnDelete(DeleteBehavior.ClientSetNull);


            builder.HasOne(x => x.ProjectTrace)
              .WithMany(x => x.EmployeeProjectTrace)
              .HasForeignKey(x => x.ProjectId)
              .HasConstraintName("FK_EmployeeProject_Project")
              .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
