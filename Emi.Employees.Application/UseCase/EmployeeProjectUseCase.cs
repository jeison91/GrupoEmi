using AutoMapper;
using Emi.Common.Exceptions;
using Emi.Common.ResponseModel;
using Emi.Employees.Application.DTO;
using Emi.Employees.Application.Port;
using Emi.Employees.Domain.Entities;
using Emi.Employees.Domain.IRepository;
using System.Text.Json;

namespace Emi.Employees.Application.UseCase
{
    public class EmployeeProjectUseCase(IEmployeeProjectRepository _employeeProjectRepository,
        IProjectRepository _projectRepository,
        IEmployeeRepository _employeeRepository,
        IMapper _mapper) : IEmployeeProjectPort
    {
        public async Task RegisterEmployeeProject(int employeeId, int projectId)
        {
            if (!await _projectRepository.ExistProject(projectId))
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = "El Proyecto enviado no es válido." }));

            if (!await _employeeRepository.Exist(employeeId))
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = "El Empleado enviado no es válido." }));

            if (await _employeeProjectRepository.ExistRelationEmployeeProject(employeeId, projectId))
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = $"El Empleado: {employeeId} ya está relacionado al Proyecto: {projectId}." }));

            var employeeProjectEntity = new EmployeeProjectEntity() { EmployeeId = employeeId, ProjectId = projectId };
            await _employeeProjectRepository.RegisterEmployeeProject(employeeProjectEntity);
        }

        public async Task<List<EmployeeResponseDTO>> GetEmployeeByDepartment(int idDepartment)
        {
            var entity = await _employeeRepository.GetEmployeesByDepartment(idDepartment);
            var employees = _mapper.Map<List<EmployeeResponseDTO>>(entity);
            return employees;
        }
    }
}
