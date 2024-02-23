using AlcaldiaApp.Models;
using AlcaldiaApp.Repositories;
using AlcaldiaApp.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AlcaldiaApp.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly IValidator<serviceRequestModel> _serviceRequestValidator;
        private readonly IServiceRequestRepository _serviceRequestRepository;

        public ServiceRequestController(IServiceRequestRepository serviceRequestRepository, IValidator<ServiceRequestModel> serviceRequestValidator)
        {
            _serviceRequestRepository = serviceRequestRepository;
            _serviceRequestValidator = serviceRequestValidator;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceRequestModel serviceRequestModel)
        {
            ValidationResult validationResult = _serviceRequestValidator.Validate(serviceRequestModel);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(serviceRequestModel);
            }
            _serviceRequestRepository.Add(serviceRequestModel);
            TempData["SuccessMessage"] = "El registro se creo exitosamente.";
            return RedirectToAction(nameof(Index));
        }


        // Edit
        [HttpGet]
        public IActionResult Edit(int id)
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
        public IActionResult Edit(ServiceRequestModel serviceRequestModel)
        {
            ValidationResult validationResult = _serviceRequestValidator.Validate(serviceRequestModel);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(serviceRequestModel);
            }
            _serviceRequestRepository.Edit(serviceRequestModel);
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
        public IActionResult Delete(ServiceRequestModel serviceRequestModel)
        {
            _serviceRequestRepository.Delete(serviceRequestModel.Id);
            TempData["SuccessMessage"] = "El registro se Elimino exitosamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
