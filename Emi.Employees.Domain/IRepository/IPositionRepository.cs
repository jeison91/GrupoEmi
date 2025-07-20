using Emi.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Domain.IRepository
{
    public interface IPositionRepository
    {
        Task<PositionEntity> GetById(int id);
    }
}
