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
    public class EmployeeRepository(EmiDbContext _context) : IEmployeeRepository
    {
        public async Task Create(EmployeeEntity entity)
        {
            await _context.Employees.AddAsync(entity);
            //await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int Id)
        {
            var deleteEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id.Equals(Id));
            if (deleteEmployee != null)
            {
                _context.Employees.Remove(deleteEmployee);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Exist(int Id) => await _context.Employees.AsNoTracking().AnyAsync(e => e.Id == Id);

        public async Task<List<EmployeeEntity>> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<EmployeeEntity> Employees;
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                Employees = _context.Employees.AsNoTracking()
                    //.Include(x => x.CurrentPosition)
                    //.Include(x => x.PositionHistories)
                    .OrderBy(x => x.Id)
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else {
                Employees = _context.Employees.AsNoTracking()
                    //.Include(x => x.CurrentPosition)
                    //.Include(x => x.PositionHistories)
                    .OrderBy(x => x.Id);
            }

            return await Employees.ToListAsync();
        }

        public async Task<EmployeeEntity> GetById(int Id)
            => await _context.Employees.AsNoTracking().FirstAsync(x => x.Id == Id);

        public async Task Update(EmployeeEntity entity)
        {
            //_context.Entry(entity).State = EntityState.Modified;
            _context.Employees.Update(entity);
            //await _context.SaveChangesAsync();
        }
    }
}
