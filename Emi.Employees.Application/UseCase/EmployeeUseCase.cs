using AutoMapper;
using Emi.Common.Exceptions;
using Emi.Common.ResponseModel;
using Emi.Employees.Application.DTO;
using Emi.Employees.Application.Port;
using Emi.Employees.Domain.Entities;
using Emi.Employees.Domain.IRepository;
using Emi.Employees.Domain.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Emi.Employees.Application.UseCase
{
    public class EmployeeUseCase(IEmployeeRepository _employeeRepository, IPositionRepository _positionRepository,
        IPositionHistoryRepository _positionHistory, IUnitOfWork _unitofWork, IMapper _mapper) : IEmployeePort
    {
        public async Task Add(EmployeeDTO employee)
        {
            var entity = _mapper.Map<EmployeeEntity>(employee);
            if (await _employeeRepository.Exist(entity.Id))
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = $"Ya existe un empleado con el número de identificación: {entity.Id}" }));
            if ((await _positionRepository.GetById(employee.CurrentPosition)) is null)
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = "El Puesto enviado no es válido." }));

            await _employeeRepository.Create(entity);
            await InsertPositionHistory(entity);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            if (!await _employeeRepository.Delete(Id))
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = $"No se encontro el empleado con Identificación: {Id}" }));
        }

        public async Task<List<EmployeeResponseDTO>> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            var employees = await _employeeRepository.GetAll(pageNumber, pageSize);
            var MapEmployee = _mapper.Map<List<EmployeeResponseDTO>>(employees);

            //List<EmployeeResponseDTO> MapEmployee = [];
            return MapEmployee;
        }

        public async Task<EmployeeResponseDTO> GetById(int Id)
        {
            var employees = await _employeeRepository.GetById(Id) ??
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = "No se encontro el empleado." }));
            var MapEmployee = _mapper.Map<EmployeeResponseDTO>(employees);
            return MapEmployee;
        }

        public async Task Update(EmployeeDTO employee)
        {
            await GetById(employee.Id);
            var entity = _mapper.Map<EmployeeEntity>(employee);
            await _employeeRepository.Update(entity);
            await InsertPositionHistory(entity);
            await _unitofWork.SaveChangesAsync();
        }

        private async Task InsertPositionHistory(EmployeeEntity employee)
        {
            PositionHistoryEntity positionHistory = new()
            {
                EmployeeId = employee.Id,
                PositionId = employee.CurrentPosition,
                StartDate = DateTime.Now
            };
            await _positionHistory.Create(positionHistory);
        }

        public async Task<List<EmployeeBonusDTO>> CalculateBonusEmployee()
        {
            var employeesEntity = await _employeeRepository.GetAll();
            var EmployeeBonus = employeesEntity.Select(x => new EmployeeBonusDTO
            {
                Id = x.Id,
                Name = x.Name,
                Position = x.PositionTrace!.Name,
                Salary = x.Salary,
                Bonus = x.PositionTrace.IsManager ? x.Salary * 0.20m : x.Salary * 0.10m
            }).ToList();

            return EmployeeBonus;
        }
    }
}
