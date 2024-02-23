using AlcaldiaApp.Models;
using FluentValidation;

namespace AlcaldiaApp.Validations
{
    public class EmployeeValidator : AbstractValidator<EmployeeModel>
    {
        public EmployeeValidator() 
        { 
            RuleFor(employee => employee.FirstName)
                .NotEmpty().WithMessage("El nombre es Requerido").WithName("Nombre")
                .NotNull().WithMessage("El nombre no puede estar Vacio")
                .MinimumLength(4).WithMessage("Debe ingresar al menos 4 caracteres")
                .MaximumLength(75).WithMessage("Solo puede ingersar un maximo de 75 caracteres");

            RuleFor(employee => employee.LastName)
                .NotEmpty().WithMessage("El apellido es Requerido").WithName("Apellido")
                .NotNull().WithMessage("El apellido no puede estar Vacio")
                .MinimumLength(4).WithMessage("Debe ingresar al menos 4 caracteres")
                .MaximumLength(75).WithMessage("Solo puede ingersar un maximo de 75 caracteres");

            RuleFor(employee => employee.PositionId)
                .NotEmpty().WithMessage("El Cargo es Requerido").WithName("Cargo")
                .NotNull().WithMessage("El Cargo no puede estar Vacio");

            RuleFor(employee => employee.Salary)
                .NotEmpty().WithMessage("El salario es Requerido")
                .NotNull().WithMessage("El salario no puede estar Vacio").WithName("Salario")
                .GreaterThan(0).WithMessage("El Salario debe ser mayor a 0");
        }
    }
}
