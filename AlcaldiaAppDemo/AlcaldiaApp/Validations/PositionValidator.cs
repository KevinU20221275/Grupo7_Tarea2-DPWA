using AlcaldiaApp.Models;
using FluentValidation;

namespace AlcaldiaApp.Validations
{
    public class PositionValidator : AbstractValidator<PositionModel>
    {
        public PositionValidator() 
        {
            RuleFor(position => position.Position)
                .NotNull().WithMessage("El cargo no debe estar vacio")
                .NotEmpty().WithMessage("El Cargo es Requerido").WithName("Cargo")
                .MinimumLength(2).WithMessage("Debe Ingresar Almenos 2 Caracteres")
                .MaximumLength(100).WithMessage("Solo puede ingresar un maximo de 100 caracteres");

            RuleFor(position => position.Description)
                .NotNull().WithMessage("Debe agregar una descripcion")
                .NotEmpty().WithMessage("Una descripcion es Requerida").WithName("Descripcion")
                .MinimumLength(4).WithMessage("Debe ingresar al menos 4 caracteres")
                .MaximumLength(250).WithMessage("Solo puede ingresar un maximo de 250 caracteres");
        }
    }
}
