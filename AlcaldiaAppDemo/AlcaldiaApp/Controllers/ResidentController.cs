using AlcaldiaApp.Models;
using AlcaldiaApp.Repositories;
using AlcaldiaApp.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlcaldiaApp.Controllers
{
    public class ResidentController : Controller
    {
        private readonly IResidentRepository _residentRepository;

        private readonly IValidator<ResidentModel> _residentValidator;

        public ResidentController(IResidentRepository residentRepository, IValidator<ResidentModel> residentValidator)
        {
            _residentRepository = residentRepository;

            _residentValidator = residentValidator;
        }

        // Index
        public IActionResult Index()
        {
            return View(_residentRepository.GetAllResidents());
        }


        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ResidentModel resident)
        {
            ValidationResult validationResult = _residentValidator.Validate(resident);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);

                return View(resident);
            }
            _residentRepository.Add(resident);
            TempData["SuccessMessage"] = "El registro se creo exitosamente.";

            return RedirectToAction(nameof(Index));
        }


        // Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _residentRepository.GetResidentById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ResidentModel resident)
        {
            ValidationResult validationResult = _residentValidator.Validate(resident);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);

                return View(resident);
            }

            _residentRepository.Edit(resident);
            TempData["SuccessMessage"] = "El registro se edito exitosamente.";

            return RedirectToAction(nameof(Index));
        }


        // Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var resident = _residentRepository.GetResidentById(id);
            if (resident == null)
            {
                return NotFound();
            }
            return View(resident);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ResidentModel resident)
        {
            _residentRepository.Delete(resident.Id);
            TempData["SuccessMessage"] = "El registro se elimino exitosamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
