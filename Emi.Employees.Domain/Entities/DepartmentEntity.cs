using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Domain.Entities
{
    public class DepartmentEntity
    {
        public required int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<PositionEntity> DepartmentPositionsTrace { get; set; }
    }
}
