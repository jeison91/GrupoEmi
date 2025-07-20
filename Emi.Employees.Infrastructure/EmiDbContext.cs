using Emi.Employees.Domain.Entities;
using Emi.Employees.Domain.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Infrastructure
{
    public class EmiDbContext(DbContextOptions<EmiDbContext> option) : DbContext(option), IUnitOfWork
    {
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<PositionEntity> Positions { get; set; }
        public DbSet<PositionHistoryEntity> PositionHistories { get; set; } 
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        //public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        //public DbSet<User> Users { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (optionsBuilder.IsConfigured)
            //{
            //    SqlServerOptionsExtension CnxOptios = optionsBuilder.Options.Extensions.OfType<SqlServerOptionsExtension>().First();
            //    string? cnx = CnxOptios.ConnectionString;

            //    if (cnx != null)
            //        optionsBuilder.UseSqlServer(cnx);//.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmiDbContext).Assembly);

            // Agregamos datos a la tablas maestras.
            modelBuilder.Entity<DepartmentEntity>().HasData(
                new DepartmentEntity { Id = 1, Name = "Gerencia" },
                new DepartmentEntity { Id = 2, Name = "Contabilidad" },
                new DepartmentEntity { Id = 3, Name = "Tecnología" }
            );

            modelBuilder.Entity<PositionEntity>().HasData(
                new PositionEntity { Id = 1, Name = "Presidente" },
                new PositionEntity { Id = 2, Name = "Director Tecnología" },
                new PositionEntity { Id = 3, Name = "Director Contable" },
                new PositionEntity { Id = 3, Name = "Director Financiero" },
                new PositionEntity { Id = 3, Name = "Ingeniros" }
            );

            modelBuilder.Entity<ProjectEntity>().HasData(
                new PositionEntity { Id = 1, Name = "Proyecto 1" },
                new PositionEntity { Id = 2, Name = "Proyecto 2" },
                new PositionEntity { Id = 3, Name = "Proyecto 3" },
                new PositionEntity { Id = 3, Name = "Proyecto 4" },
                new PositionEntity { Id = 3, Name = "Proyecto 5" }
            );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
