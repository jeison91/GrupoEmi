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
    public class PositionHistoryRepository(EmiDbContext _context) : IPositionHistoryRepository
    {
        public async Task Create(PositionHistoryEntity entity)
        {
            var Employees = await _context.PositionHistories.AsNoTracking().Where(x => x.EmployeeId == entity.EmployeeId).ToListAsync();
            if (Employees.Count > 0)
            {
                var lastEmployee = Employees.Last();
                if (lastEmployee.PositionId != entity.PositionId)
                {
                    lastEmployee.EndDate = entity.StartDate;
                    _context.PositionHistories.Update(entity);
                }
            }
            else
            {
                await _context.PositionHistories.AddAsync(entity);
            }
            //await _context.SaveChangesAsync();
        }
    }
}
