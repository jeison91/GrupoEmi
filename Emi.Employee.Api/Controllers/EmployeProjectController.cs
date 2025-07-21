using Emi.Common.ResponseModel;
using Emi.Employees.Application.Port;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emi.Employee.Api.Controllers
{
    /// <summary>
    /// Endpoint que se encarga de la relación entre empleado y proyectos
    /// </summary>
    /// <param name="_employeeProjectPort"></param>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADM")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
    public class EmployeProjectController(IEmployeeProjectPort _employeeProjectPort) : ControllerBase
    {
        /// <summary>
        /// Metodo encargado de registrar un proyecto a un empleado
        /// </summary>
        /// <param name="IdEmployee"></param>
        /// <param name="IdProject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] int IdEmployee, [FromQuery] int IdProject)
        {
            await _employeeProjectPort.RegisterEmployeeProject(IdEmployee, IdProject);
            return Ok(new MessageResponse() { Status = StatusCodes.Status201Created, Message = "Registro Exitoso" });
        }

        /// <summary>
        /// Metodo encargado de mostrar los empleados asociados a departamento y tiene proyecto asociado.
        /// </summary>
        /// <param name="Department"></param>
        /// <returns></returns>
        [HttpGet("{Department}")]
        public async Task<IActionResult> GetEmployeeByDepartment(int Department)
        {
            var result = await _employeeProjectPort.GetEmployeeByDepartment(Department);
            return Ok(new MessageResponse() { Status = StatusCodes.Status201Created, Message = "Consulta finalizada.", Data = result });
        }
    }
}
