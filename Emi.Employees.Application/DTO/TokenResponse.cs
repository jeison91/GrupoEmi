using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Application.DTO
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public int ExpireIn { get; set; }
        public string Token_type { get; set; }

    }
}
