using AlcaldiaApp.Models;
using AlcaldiaApp.Repositories;
using AlcaldiaApp.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AlcaldiaApp.Controllers
{
    public class PositionController : Controller
    {
        private readonly IValidator<PositionModel> _positionValidator;
        private readonly IPositionRepository _positionRepository;

        public PositionController(IPositionRepository positionRepository, IValidator<PositionModel> positionValidator)
        {
            _positionRepository = positionRepository;
            _positionValidator = positionValidator;
        }


        // Index
        public IActionResult Index()
        {
            return View(_positionRepository.GetAllPositions());
        }


        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PositionModel positionModel)
        {
            ValidationResult validationResult = _positionValidator.Validate(positionModel);
            if (!validationResult.IsValid)
            {
                 validationResult.AddToModelState(this.ModelState);
                 return View(positionModel);
            }
            _positionRepository.Add(positionModel);
            TempData["SuccessMessage"] = "El registro se creo exitosamente.";
            return RedirectToAction(nameof(Index)); 
        }


        // Edit
        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var position = _positionRepository.GetPositionById(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PositionModel positionModel)
        {
            ValidationResult validationResult = _positionValidator.Validate(positionModel);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(positionModel);
            }
            _positionRepository.Edit(positionModel);
            TempData["SuccessMessage"] = "El registro se Edito exitosamente.";
            return RedirectToAction(nameof(Index));
        }


        // Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var position = _positionRepository.GetPositionById(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PositionModel positionModel)
        {
            _positionRepository.Delete(positionModel.Id);
            TempData["SuccessMessage"] = "El registro se Elimino exitosamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
