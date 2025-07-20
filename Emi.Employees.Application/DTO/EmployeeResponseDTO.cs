using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Application.DTO
{
    public class EmployeeResponseDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Position { get; set; }
        public required decimal Salary { get; set; }
    }
}
