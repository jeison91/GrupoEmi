using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Application.DTO
{
    public class EmployeeBonusDTO : EmployeeResponseDTO
    {
        public required decimal Bonus { get; set; }
    }
}
