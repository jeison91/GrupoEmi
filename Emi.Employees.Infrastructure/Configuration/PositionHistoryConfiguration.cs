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
    public class PositionHistoryConfiguration : IEntityTypeConfiguration<PositionHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<PositionHistoryEntity> builder)
        {
            builder.ToTable("PositionHistories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.EmployeeId).IsRequired();
            builder.Property(x => x.PositionId).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired(false);

            // Relación con Employee
            builder.HasOne(x => x.Employee)
                   .WithMany(x => x.PositionHistoryTrace)
                   .HasForeignKey(x => x.EmployeeId)
                   .HasConstraintName("FK_PositionHistory_Employee")
                   .OnDelete(DeleteBehavior.Cascade);

            // Relación con Position
            builder.HasOne(x => x.Position)
                   .WithMany()
                   .HasForeignKey(x => x.PositionId)
                   .HasConstraintName("FK_PositionHistory_Position")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
