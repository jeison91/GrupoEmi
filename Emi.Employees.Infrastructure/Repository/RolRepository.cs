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
    public class RolRepository(EmiDbContext _context) : IRolRepository
    {
        public async Task<UserRolEntity?> GetById(int id)
            => await _context.UserRols.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
