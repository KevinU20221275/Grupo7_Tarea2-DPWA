using AlcaldiaApp.Models;
using FluentValidation;

namespace AlcaldiaApp.Validations
{
    public class ResidentValidator : AbstractValidator<ResidentModel>
    {
        public ResidentValidator()
        {
            RuleFor(resident => resident.FirstName)
                .NotEmpty().WithMessage("El nombre es Requerido").WithName("Nombres")
                .NotNull().WithMessage("El nombre no puede estar Vacio")
                .MinimumLength(4).WithMessage("Debe ingresar al menos 2 caracteres")
                .MaximumLength(75).WithMessage("Solo puede ingersar un maximo de 75 caracteres");

            RuleFor(resident => resident.LastName)
                .NotEmpty().WithMessage("El apellido es Requerido").WithName("Apellidos")
                .NotNull().WithMessage("El apellido no puede estar Vacio")
                .MinimumLength(4).WithMessage("Debe ingresar al menos 2 caracteres")
                .MaximumLength(75).WithMessage("Solo puede ingersar un maximo de 75 caracteres");

            RuleFor(resident => resident.Address)
                .NotEmpty().WithMessage("La Direccion es Requerida").WithName("Direccion")
                .NotNull().WithMessage("La Direccion no puede estar Vacia")
                .MaximumLength(250).WithMessage("Solo puede ingresar un maximo de 250 caracteres");

            RuleFor(resident => resident.BirthDate)
                .NotEmpty().WithMessage("La Fecha de Nacimiento es Requerida")
                .NotNull().WithMessage("La Fecha de Nacimiento no puede estar Vacia").WithName("Fecha de Nacimiento");
        }
    }
}
