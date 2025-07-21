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
    public class PositionConfiguration : IEntityTypeConfiguration<PositionEntity>
    {
        public void Configure(EntityTypeBuilder<PositionEntity> builder)
        {
            builder.ToTable("Positions");
            builder.HasKey(x => x.Id );
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.IsManager).IsRequired();
            builder.Property(x => x.Department).IsRequired();

            builder.HasMany(x => x.Employees)
                .WithOne(x => x.PositionTrace)
                .HasForeignKey(x => x.CurrentPosition);

            builder.HasOne(x => x.DepartmentTrace)
              .WithMany(x => x.DepartmentPositionsTrace)
              .HasForeignKey(x => x.Department)
              .HasConstraintName("FK_Positions_Department")
              .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
