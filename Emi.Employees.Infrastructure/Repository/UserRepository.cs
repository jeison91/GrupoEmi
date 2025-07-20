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
    public class UserRepository(EmiDbContext _context) : IUserRepository
    {
        public async Task Create(UserEntity entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserEntity?> GetByUserName(string username)
        {
            var result = await _context.Users
                .Include(x => x.RoleTrace)
                .Where(x => x.Username.Equals(username))
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
