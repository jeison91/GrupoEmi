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

        public async Task<bool> Exist(int Id)
            => await _context.Employees.AsNoTracking().AnyAsync(e => e.Id == Id);

        public async Task<List<EmployeeEntity>> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<EmployeeEntity> Employees;
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                Employees = _context.Employees.AsNoTracking()
                    .Include(x => x.PositionTrace)
                    .Include(x => x.PositionHistoryTrace)
                    .OrderBy(x => x.Id)
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else {
                Employees = _context.Employees.AsNoTracking()
                    .Include(x => x.PositionTrace)
                    .Include(x => x.PositionHistoryTrace)
                    .OrderBy(x => x.Id);
            }

            return await Employees.ToListAsync();
        }

        public async Task<EmployeeEntity?> GetById(int Id)
            => await _context.Employees.AsNoTracking().Include(x => x.PositionTrace).FirstOrDefaultAsync(x => x.Id == Id);

        public async Task Update(EmployeeEntity entity)
        {
            _context.Employees.Update(entity);
        }

        public async Task<List<EmployeeEntity>> GetEmployeesByDepartment(int idDeparment)
        {
            var queryEmployees = _context.Employees
                .Include(x => x.PositionTrace)
                .ThenInclude(x => x.DepartmentTrace)
                .Include(x => x.EmployeeProjectTrace).AsNoTracking();

            queryEmployees = queryEmployees.Where(x =>
                x.PositionTrace != null &&
                x.PositionTrace.DepartmentTrace.Id.Equals(idDeparment));

            queryEmployees = queryEmployees.Select(x => new EmployeeEntity
            {
                Id = x.Id,
                Name = x.Name,
                Salary = x.Salary,
                PositionTrace = new PositionEntity { Name = x.PositionTrace.Name },
                PositionHistoryTrace = x.PositionHistoryTrace,
                EmployeeProjectTrace = x.EmployeeProjectTrace
            });
            return await queryEmployees.OrderBy(x => x.Id).ToListAsync();
        }
    }
}
