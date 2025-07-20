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
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id );
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Username).HasMaxLength(20).IsRequired(true);
            builder.Property(x => x.Password).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Role).IsRequired();

            builder.HasOne(x => x.RoleTrace)
               .WithMany(x => x.UsersTrace)
               .HasForeignKey(x => x.Role)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
