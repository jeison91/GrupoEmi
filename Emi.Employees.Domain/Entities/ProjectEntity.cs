﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Domain.Entities
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public virtual ICollection<EmployeeProjectEntity> EmployeeProjectTrace { get; set; }
    }
}
