using AlcaldiaApp.Models;
using AlcaldiaApp.Repositories;
using AlcaldiaApp.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AlcaldiaApp.Controllers
{
    public class MunicipalServiceController : Controller
    {
        private readonly IMunicipalServiceRepository _municipalServiceRepository;

        private readonly IValidator<MunicipalServiceModel> _municipalServiceValidator;

        public MunicipalServiceController(IMunicipalServiceRepository municipalServiceRepository, IValidator<MunicipalServiceModel> municipalServiceValidator)
        {
            _municipalServiceRepository = municipalServiceRepository;

            _municipalServiceValidator = municipalServiceValidator;
        }

        // Index
        public IActionResult Index()
        {
            return View(_municipalServiceRepository.GetAllMunicipalServices());
        }


        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MunicipalServiceModel municipalService)
        {
            ValidationResult validationResult = _municipalServiceValidator.Validate(municipalService);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);

                return View(municipalService);
            }
            _municipalServiceRepository.Add(municipalService);
            TempData["SuccessMessage"] = "El registro se creo exitosamente.";

            return RedirectToAction(nameof(Index));
        }


        // Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var municipalService = _municipalServiceRepository.GetMunicipalServiceById(id);

            if (municipalService == null)
            {
                return NotFound();
            }

            return View(municipalService);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MunicipalServiceModel municipalService)
        {
            ValidationResult validationResult = _municipalServiceValidator.Validate(municipalService);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);

                return View(municipalService);
            }

            _municipalServiceRepository.Edit(municipalService);
            TempData["SuccessMessage"] = "El registro se edito exitosamente.";

            return RedirectToAction(nameof(Index));
        }


        // Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var municipalService = _municipalServiceRepository.GetMunicipalServiceById(id);
            if (municipalService == null)
            {
                return NotFound();
            }
            return View(municipalService);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(MunicipalServiceModel municipalService)
        {
            _municipalServiceRepository.Delete(municipalService.Id);
            TempData["SuccessMessage"] = "El registro se elimino exitosamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
