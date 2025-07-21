using Emi.Employees.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Application.Port
{
    public interface IEmployeeProjectPort
    {
        Task RegisterEmployeeProject(int employeeId, int projectId);
        Task<List<EmployeeResponseDTO>> GetEmployeeByDepartment(int idDepartment);
    }
}
