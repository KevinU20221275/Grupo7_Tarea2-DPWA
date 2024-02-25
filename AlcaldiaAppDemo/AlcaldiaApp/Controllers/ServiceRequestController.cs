using AlcaldiaApp.Models;
using AlcaldiaApp.Repositories;
using AlcaldiaApp.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AlcaldiaApp.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly IValidator<ServiceRequestModel> _serviceRequestValidator;
        private readonly IServiceRequestRepository _serviceRequestRepository;
        private SelectList _residentsList;
        private SelectList _munisipalServicesList;

        public ServiceRequestController(IServiceRequestRepository serviceRequestRepository, IValidator<ServiceRequestModel> serviceRequestValidator)
        {
            _serviceRequestRepository = serviceRequestRepository;
            _serviceRequestValidator = serviceRequestValidator;

            var residents = _serviceRequestRepository.GetAllResidents();
            _residentsList = new SelectList(residents,
                                                nameof(ResidentModel.Id),
                                                nameof(ResidentModel.FirstName));

            var municipalServices = _serviceRequestRepository.GetAllMunicipalServices();   // carga la lista de Servicios Municipales para el GET y POST de Create
            _munisipalServicesList = new SelectList(municipalServices,
                                                nameof(MunicipalServiceModel.Id),
                                                nameof(MunicipalServiceModel.ServiceName));
        }


        // Index
        public IActionResult Index()
        {
            return View(_serviceRequestRepository.GetAllServiceRequests());
        }



        // Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Residents = _residentsList;

            ViewBag.MunicipalServices = _munisipalServicesList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceRequestModel serviceRequest)
        {
            ValidationResult validationResult = _serviceRequestValidator.Validate(serviceRequest);
            if (!validationResult.IsValid)
            {
                ViewBag.Residents = _residentsList;

                ViewBag.MunicipalServices = _munisipalServicesList;

                validationResult.AddToModelState(this.ModelState);
                return View(serviceRequest);
            }
            _serviceRequestRepository.Add(serviceRequest);
            TempData["SuccessMessage"] = "El registro se creo exitosamente.";
            return RedirectToAction(nameof(Index));
        }


        // Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var serviceRequest = _serviceRequestRepository.GetServiceRequestById(id);

            var residents = _serviceRequestRepository.GetAllResidents(); // carga la lista de residentes
            
            var municipalServices = _serviceRequestRepository.GetAllMunicipalServices(); // carga la lista servicios municipales
            
            if (serviceRequest == null)
            {
                return NotFound();
            }

            ViewBag.Residents = new SelectList(residents,
                                                nameof(ResidentModel.Id),
                                                nameof(ResidentModel.FirstName),
                                                serviceRequest.Id);

            ViewBag.MunicipalServices = new SelectList(municipalServices,
                                                nameof(MunicipalServiceModel.Id),
                                                nameof(MunicipalServiceModel.ServiceName),
                                                serviceRequest.Id);
            return View(serviceRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ServiceRequestModel serviceRequest)
        {
            ValidationResult validationResult = _serviceRequestValidator.Validate(serviceRequest);
            if (!validationResult.IsValid)
            {
                var residents = _serviceRequestRepository.GetAllResidents(); // carga la lista de residentes

                var municipalServices = _serviceRequestRepository.GetAllMunicipalServices(); // carga la lista servicios municipales

                ViewBag.Residents = new SelectList(residents,
                                                nameof(ResidentModel.Id),
                                                nameof(ResidentModel.FirstName),
                                                serviceRequest.Id);

                ViewBag.MunicipalServices = new SelectList(municipalServices,
                                                    nameof(MunicipalServiceModel.Id),
                                                    nameof(MunicipalServiceModel.ServiceName),
                                                    serviceRequest.Id);

                validationResult.AddToModelState(this.ModelState);
                return View(serviceRequest);
            }
            _serviceRequestRepository.Edit(serviceRequest);
            TempData["SuccessMessage"] = "El registro se Edito exitosamente.";
            return RedirectToAction(nameof(Index));
        }


        // Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var serviceRequest = _serviceRequestRepository.GetServiceRequestById(id);
            if (serviceRequest == null)
            {
                return NotFound();
            }
            return View(serviceRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ServiceRequestModel serviceRequest)
        {
            _serviceRequestRepository.Delete(serviceRequest.Id);
            TempData["SuccessMessage"] = "El registro se Elimino exitosamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
