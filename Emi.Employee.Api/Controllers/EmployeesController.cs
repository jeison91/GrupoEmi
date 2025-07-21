using Emi.Common.ResponseModel;
using Emi.Employees.Application.DTO;
using Emi.Employees.Application.Port;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Emi.Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
    public class EmployeesController(IEmployeePort _employeePort) : ControllerBase
    {
        /// <summary>
        /// Metodo encargado de obtener todos los empleados.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "ADM,USR")]
        public async Task<IActionResult> Get([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var employees = await _employeePort.GetAll(pageNumber, pageSize);
            return Ok(new MessageResponse() { Status = StatusCodes.Status201Created, Message = "Consulta finalizada.", Data = employees });
        }

        /// <summary>
        /// Metodo encargado de buscar un empleado por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "ADM")]
        public async Task<IActionResult> Get(int id)
        {
            var employees = await _employeePort.GetById(id);
            return Ok(new MessageResponse() { Status = StatusCodes.Status201Created, Message = "Consulta finalizada.", Data = employees });
        }

        /// <summary>
        /// Metodo encargo de registrar un empleado en el sistema.
        /// </summary>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "ADM")]
        public async Task<IActionResult> Post([FromBody] EmployeeDTO employeeDTO)
        {
            EmployeeDTOValidator validator = new();
            var validatorResult = await validator.ValidateAsync(employeeDTO);
            if (!validatorResult.IsValid)
                InvalidModel.Response(validatorResult);

            await _employeePort.Add(employeeDTO);
            return Ok(new MessageResponse() { Status = StatusCodes.Status201Created, Message = "Registro Exitoso" });
        }

        /// <summary>
        /// Metodo para actualizar la información del empleado que se especifique.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADM")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            employeeDTO.Id = id;
            EmployeeDTOValidator validator = new();
            var validatorResult = await validator.ValidateAsync(employeeDTO);
            if (!validatorResult.IsValid)
                InvalidModel.Response(validatorResult);

            await _employeePort.Update(employeeDTO);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Registro actualizado" });
        }

        /// <summary>
        /// Metodo encargado de eliminar un empleado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADM")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeePort.Delete(id);
            return Ok(new MessageResponse() { Status = StatusCodes.Status204NoContent, Message = "Registro eliminado" });
        }

        /// <summary>
        /// Metodo encargado de mostrar el bono por empleado acorde al salario.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AnualCalculateBonus")]
        [Authorize(Roles = "ADM")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
        public async Task<IActionResult> AnualCalculateBonus()
        {
            var result = await _employeePort.CalculateBonusEmployee();
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada", Data = result });
        }
    }
}
