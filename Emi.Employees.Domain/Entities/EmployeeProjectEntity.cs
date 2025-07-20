using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Domain.Entities
{
    public class EmployeeProjectEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public EmployeeEntity EmployeeTrace { get; set; }
        public ProjectEntity ProjectTrace { get; set; }
    }
}
