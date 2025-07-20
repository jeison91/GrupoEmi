using Emi.Common.ResponseModel;
using Emi.Employees.Application.DTO;
using Emi.Employees.Application.Port;
using Microsoft.AspNetCore.Mvc;

namespace Emi.Employee.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_userPort"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserPort _userPort) : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(UserRequest userRequest)
        {
            UserRequestValidator validator = new();
            var validatorResult = await validator.ValidateAsync(userRequest);
            if (!validatorResult.IsValid)
                InvalidModel.Response(validatorResult);

            await _userPort.Create(userRequest);
            return Ok(new MessageResponse() { Status = StatusCodes.Status201Created, Message = "Registro usuario exitoso" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromBody] TokenRequest tokenRequest)
        {
            var token = await _userPort.Login(tokenRequest);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Logeo existoso", Data = token });
        }
    }
}
