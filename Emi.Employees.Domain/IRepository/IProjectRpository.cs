using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Domain.IRepository
{
    public interface IProjectRepository
    {
        Task<bool> ExistProject(int id);
    }
}
