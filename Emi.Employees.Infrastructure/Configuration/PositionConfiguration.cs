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

            builder.HasMany(p => p.Employees)
                .WithOne(e => e.PositionTrace)
                .HasForeignKey(e => e.CurrentPosition);
        }
    }
}
