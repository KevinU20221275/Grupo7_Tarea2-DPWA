using AlcaldiaApp.Models;
using FluentValidation;

namespace AlcaldiaApp.Validations
{
    public class MunicipalServiceValidator : AbstractValidator<MunicipalServiceModel>
    {
        public MunicipalServiceValidator()
        {
            RuleFor(municipalService => municipalService.ServiceName)
                .NotEmpty().WithMessage("El nombre del Servicio es Requerido").WithName("Nombre del Servicio")
                .NotNull().WithMessage("El nombre del Servicio no puede estar Vacio")
                .MinimumLength(4).WithMessage("Debe ingresar al menos 2 caracteres")
                .MaximumLength(100).WithMessage("Solo puede ingersar un maximo de 100 caracteres");

            RuleFor(municipalService => municipalService.Description)
                .NotEmpty().WithMessage("La Descripcion es Requerida").WithName("Descripcion")
                .NotNull().WithMessage("La Descripcion no puede estar Vacia")
                .MinimumLength(4).WithMessage("Debe ingresar al menos 4 caracteres")
                .MaximumLength(250).WithMessage("Solo puede ingersar un maximo de 250 caracteres");


            RuleFor(municipalService => municipalService.Cost)
                .NotEmpty().WithMessage("El salario es Requerido")
                .NotNull().WithMessage("El salario no puede estar Vacio").WithName("Salario")
                .GreaterThan(-1).WithMessage("El Costo debe ser mayor o igual a 0");
        }
    }
}
