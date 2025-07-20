using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Domain.Entities
{
    public class EmployeeEntity
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public int CurrentPosition { get; set; }
        public decimal Salary { get; set; }


        public virtual PositionEntity? PositionTrace { get; set; }
        public virtual ICollection<PositionHistoryEntity> PositionHistoryTrace { get; set; } = [];
    }
}
