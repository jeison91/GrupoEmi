using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Domain.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<T> GetById(int Id);
        Task Create(T entity);
        Task Update(T entity);
        Task<bool> Delete(int Id);
        Task<bool> Exist(int Id);
    }
}
