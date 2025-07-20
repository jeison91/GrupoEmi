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
    public class PositionRepository(EmiDbContext _context) : IPositionRepository
    {
        public async Task<PositionEntity?> GetById(int id)
            => await _context.Positions.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
