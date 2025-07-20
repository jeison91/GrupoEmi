using Emi.Employees.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Application.Port
{
    public interface IEmployeePort
    {
        Task<List<EmployeeResponseDTO>> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<EmployeeResponseDTO> GetById(int Id);
        Task Add(EmployeeDTO employee);
        Task Update(EmployeeDTO employee);
        Task Delete(int Id);
        Task<List<EmployeeBonusDTO>> CalculateBonusEmployee();
    }
}
