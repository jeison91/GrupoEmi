using Emi.Employees.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Infrastructure.Repository
{
    public class ProjectRepository(EmiDbContext _context) : IProjectRepository
    {
        public async Task<bool> ExistProject(int id)
            => await _context.Projects.AsNoTracking().AnyAsync(x => x.Id.Equals(id));
    }
}
