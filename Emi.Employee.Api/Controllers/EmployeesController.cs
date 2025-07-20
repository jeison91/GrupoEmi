using Emi.Common.ResponseModel;
using Emi.Employees.Application.DTO;
using Emi.Employees.Application.Port;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Emi.Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeesController(IEmployeePort _employeePort) : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var employees = await _employeePort.GetAll(pageNumber, pageSize);
            return Ok(employees);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employees = await _employeePort.GetById(id);
            return Ok(employees);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeDTO employeeDTO)
        {
            EmployeeDTOValidator validator = new();
            var validatorResult = await validator.ValidateAsync(employeeDTO);
            if (!validatorResult.IsValid)
                InvalidModel.Response(validatorResult);

            await _employeePort.Add(employeeDTO);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Registro Exitoso" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            employeeDTO.Id = id;
            EmployeeDTOValidator validator = new();
            var validatorResult = await validator.ValidateAsync(employeeDTO);
            if (!validatorResult.IsValid)
                InvalidModel.Response(validatorResult);

            await _employeePort.Update(employeeDTO);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeePort.Delete(id);
            return Ok();
        }
    }
}
