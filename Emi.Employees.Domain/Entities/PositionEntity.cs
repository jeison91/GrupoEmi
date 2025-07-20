using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Domain.Entities
{
    public class PositionEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsManager { get; set; }
        public virtual ICollection<EmployeeEntity> Employees { get; set; }
    }
}
