using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emi.Employees.Application.DTO
{
    public class EmployeeDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int CurrentPosition { get; set; }
        public required decimal Salary { get; set; }
    }

    public class EmployeeDTOValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeeDTOValidator() {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("El ID debe ser mayor que 0.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.CurrentPosition)
                .GreaterThan(0)
                .WithMessage("Debe seleccionar un cargo válido.");

            RuleFor(x => x.Salary)
                .GreaterThan(0)
                .WithMessage("El salario debe ser mayor a 0.");
        }
    }
}
