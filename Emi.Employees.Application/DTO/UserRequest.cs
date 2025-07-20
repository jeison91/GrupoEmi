using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Emi.Employees.Application.DTO
{
    public class UserRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required int UserRole { get; set; }
    }

    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El username es obligatorio.")
                .MaximumLength(20).WithMessage("El username no puede exceder los 20 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La password es obligatoria.")
                .MinimumLength(9).WithMessage("El tamaño mínimo de la contraseña debe ser 9 caracteres.")
                .Must(HaveUpperAndLowerAndDigit).WithMessage("La contraseña debe tener combinaciones entre mayúsculas, minúsculas y números.")
                .Must(HaveSpecialCharacter).WithMessage("La contraseña debe contener al menos un carácter especial.");

            RuleFor(x => x.UserRole)
                .GreaterThan(0).WithMessage("El rol del usuario debe ser mayor que 0.");
        }

        private bool HaveUpperAndLowerAndDigit(string password)
        {
            return Regex.IsMatch(password, "[a-z]") &&
                   Regex.IsMatch(password, "[A-Z]") &&
                   Regex.IsMatch(password, "[0-9]");
        }

        private bool HaveSpecialCharacter(string password)
        {
            // Puedes ajustar el conjunto de caracteres especiales aquí si deseas
            return Regex.IsMatch(password, @"[<>@!#$%^&*()_+\[\]{}?:;|'\\.,/~`\-=/]");
        }
    }
}
