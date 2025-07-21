using Emi.Employees.Domain.Entities;
using Emi.Employees.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Infrastructure.Repository
{
    public class EmployeeProjectRepository(EmiDbContext _context) : IEmployeeProjectRepository
    {
        public async Task RegisterEmployeeProject(EmployeeProjectEntity employeeProject)
        {
            await _context.EmployeeProjects.AddAsync(employeeProject);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistRelationEmployeeProject(int employee, int project)
            => await _context.EmployeeProjects.AsNoTracking()
            .AnyAsync(x => x.EmployeeId.Equals(employee) && x.ProjectId.Equals(project));
    }
}
