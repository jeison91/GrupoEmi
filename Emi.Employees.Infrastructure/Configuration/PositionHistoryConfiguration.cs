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
            builder.Property(ph => ph.PositionId).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired(false);

            // Relación con Employee
            builder.HasOne(ph => ph.Employee)
                   .WithMany(e => e.PositionHistoryTrace)
                   .HasForeignKey(ph => ph.EmployeeId)
                   .HasConstraintName("FK_PositionHistory_Employee")
                   .OnDelete(DeleteBehavior.Cascade);

            // Relación con Position
            builder.HasOne(ph => ph.Position)
                   .WithMany()
                   .HasForeignKey(ph => ph.PositionId)
                   .HasConstraintName("FK_PositionHistory_Position")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
