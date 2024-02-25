using AlcaldiaApp.Models;
using FluentValidation;

namespace AlcaldiaApp.Validations
{
    public class ServiceRequestValidator : AbstractValidator<ServiceRequestModel>
    {
        public ServiceRequestValidator()
        {
            RuleFor(serviceRequest => serviceRequest.ServiceId)
            .NotEmpty().WithMessage("El Servicio es Requerido").WithName("Servicio")
            .NotNull().WithMessage("El Servicio no puede estar Vacio");

            RuleFor(serviceRequest => serviceRequest.RequestDate)
                .NotEmpty().WithMessage("La Fecha de Solicitud es Requerida").WithName("Fecha de Solicitud")
                .NotNull().WithMessage("La Fecha de Solicitud no puede estar Vacia");

            
        }
    }
}
