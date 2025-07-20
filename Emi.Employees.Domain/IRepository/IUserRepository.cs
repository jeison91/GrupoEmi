using Emi.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Domain.IRepository
{
    public interface IUserRepository
    {
        Task Create(UserEntity entity);
        Task<UserEntity> GetByUserName(string username);
    }
}
