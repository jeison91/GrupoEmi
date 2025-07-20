using Emi.Employees.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Application.Port
{
    public interface IUserPort
    {
        Task<bool> Create(UserRequest userRequest);
        Task<TokenResponse> Login(TokenRequest userRequest);
    }
}
