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
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.Id );
            builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.CurrentPosition).IsRequired();
            builder.Property(x => x.Salary).HasColumnType("decimal(12,2)").IsRequired();

            builder.HasOne(x => x.PositionTrace)
               .WithMany(x => x.Employees)
               .HasForeignKey(x => x.CurrentPosition)
               .HasConstraintName("FK_Employee_Position")
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(x => x.PositionHistoryTrace)
               .WithOne()
               .HasForeignKey(ph => ph.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
