using Emi.Employees.Domain.Entities;
using Emi.Employees.Domain.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Data;
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
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserRolEntity> UserRols { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<EmployeeProjectEntity> EmployeeProjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmiDbContext).Assembly);

            // Agregamos datos a la tablas maestras.
            modelBuilder.Entity<DepartmentEntity>().HasData(
                new DepartmentEntity { Id = 1, Name = "Gerencia" },
                new DepartmentEntity { Id = 2, Name = "Contabilidad" },
                new DepartmentEntity { Id = 3, Name = "Tecnología" },
                new DepartmentEntity { Id = 4, Name = "Administrativo" }
            );

            modelBuilder.Entity<PositionEntity>().HasData(
                new PositionEntity { Id = 1, Name = "Presidente", IsManager = true, Department = 1 },
                new PositionEntity { Id = 2, Name = "Director Tecnología", IsManager = true, Department = 3 },
                new PositionEntity { Id = 3, Name = "Director Contable", IsManager = true, Department = 2 },
                new PositionEntity { Id = 4, Name = "Auxiliar contable", IsManager = false, Department = 2 },
                new PositionEntity { Id = 5, Name = "Director Financiero", IsManager = true, Department = 4 },
                new PositionEntity { Id = 6, Name = "Ingenieros", IsManager = false, Department = 3 },
                new PositionEntity { Id = 7, Name = "Secretaria", IsManager = false, Department = 4 }
            );

            modelBuilder.Entity<ProjectEntity>().HasData(
                new PositionEntity { Id = 1, Name = "Proyecto 1" },
                new PositionEntity { Id = 2, Name = "Proyecto 2" },
                new PositionEntity { Id = 3, Name = "Proyecto 3" },
                new PositionEntity { Id = 4, Name = "Proyecto 4" },
                new PositionEntity { Id = 5, Name = "Proyecto 5" }
            );

            modelBuilder.Entity<UserRolEntity>().HasData(
                new UserRolEntity { Id = 1, Name = "Admin", Code = "ADM" },
                new UserRolEntity { Id = 2, Name = "User", Code = "USR" }
            );

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity { Id = 1, Username = "Administrator", Password = "$2a$11$7QZLqxZsMNMfBVQ.MHgJbepYtK8bfwOvFsFxPhXftPnf.9xx6Kiwm", Role = 1 },
                new UserEntity { Id = 2, Username = "User1", Password = "$2a$11$OPMkuCk35gy6cz/vrau7N.aFnk0x7vU6UcxgFimuC28VHja1Vb.4a", Role = 2 },
                new UserEntity { Id = 3, Username = "User2", Password = "$2a$11$hzTMzUXnimQRS4yu7m/Ns.xl4.2KLbgZ4XEnzwFh/Y/dVnlRf.oca", Role = 2 }
            );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
