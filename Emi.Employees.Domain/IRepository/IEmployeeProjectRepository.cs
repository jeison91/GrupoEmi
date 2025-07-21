using Emi.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Domain.IRepository
{
    public interface IEmployeeProjectRepository
    {
        Task RegisterEmployeeProject(EmployeeProjectEntity employeeProject);
        Task<bool> ExistRelationEmployeeProject(int employee, int project);
    }
}
