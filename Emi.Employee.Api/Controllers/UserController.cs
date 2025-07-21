using Emi.Common.ResponseModel;
using Emi.Employees.Application.DTO;
using Emi.Employees.Application.Port;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emi.Employee.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_userPort"></param>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
    public class UserController(IUserPort _userPort) : ControllerBase
    {
        /// <summary>
        /// Metodo que se usa para generar un token y pueda acceder al resto de metodos.
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] TokenRequest tokenRequest)
        {
            var token = await _userPort.Login(tokenRequest);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "logueo existoso", Data = token });
        }

        /// <summary>
        /// Metodo usado para crear usuarios en el sistema.
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "ADM")]
        public async Task<IActionResult> Post(UserRequest userRequest)
        {
            UserRequestValidator validator = new();
            var validatorResult = await validator.ValidateAsync(userRequest);
            if (!validatorResult.IsValid)
                InvalidModel.Response(validatorResult);

            await _userPort.Create(userRequest);
            return Ok(new MessageResponse() { Status = StatusCodes.Status201Created, Message = "Registro usuario exitoso" });
        }
    }
}
