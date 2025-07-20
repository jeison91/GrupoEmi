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
    public class UserRolConfiguration : IEntityTypeConfiguration<UserRolEntity>
    {
        public void Configure(EntityTypeBuilder<UserRolEntity> builder)
        {
            builder.ToTable("UserRols");
            builder.HasKey(x => x.Id );
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(x => x.Code).HasMaxLength(5).IsRequired();
        }
    }
}
