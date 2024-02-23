using AlcaldiaApp.Models;
using AlcaldiaApp.Repositories;
using AlcaldiaApp.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlcaldiaApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IValidator<EmployeeModel> _employeeValidator;

        private SelectList _positionsList;

        public EmployeeController(IEmployeeRepository employeeRepository, IValidator<EmployeeModel> employeeValidator)
        {
            _employeeRepository = employeeRepository;
            _employeeValidator = employeeValidator;

            var positions = _employeeRepository.GetPositions(); // carga la lista de Cargos para el GET y POST de Create
            _positionsList = new SelectList(positions, nameof(PositionModel.Id), nameof(PositionModel.Position));
        }

        // Index
        public IActionResult Index()
        {
            return View(_employeeRepository.GetAllEmployees());
        }


        // Create
        [HttpGet]
        public IActionResult Create() 
        {
            var positions = _employeeRepository.GetPositions();

            ViewBag.Positions = _positionsList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeModel employee)
        {
            ValidationResult validationResult = _employeeValidator.Validate(employee);
            if (!validationResult.IsValid) 
            {
                ViewBag.Positions = _positionsList;

                validationResult.AddToModelState(this.ModelState);

                return View(employee);
            }
            _employeeRepository.Add(employee);
            TempData["SuccessMessage"] = "El registro se creo exitosamente.";

            return RedirectToAction(nameof(Index));
        }


        // Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            var positions = _employeeRepository.GetPositions();

            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Positions = new SelectList(positions,
                                                nameof(PositionModel.Id),
                                                nameof(PositionModel.Position),
                                                employee?.PositionId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeModel employee)
        {
            ValidationResult validationResult = _employeeValidator.Validate(employee);

            if (!validationResult.IsValid)
            {
                var positions = _employeeRepository.GetPositions(); // Cargar nuevamente las posiciones
                ViewBag.Positions = new SelectList(positions,
                                                nameof(PositionModel.Id),
                                                nameof(PositionModel.Position),
                                                employee?.PositionId);

                validationResult.AddToModelState(this.ModelState);

                return View(employee);
            }

            _employeeRepository.Edit(employee);
            TempData["SuccessMessage"] = "El registro se edito exitosamente.";

            return RedirectToAction(nameof(Index));
        }


        // Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EmployeeModel employee)
        {
            _employeeRepository.Delete(employee.Id);
            TempData["SuccessMessage"] = "El registro se elimino exitosamente.";

            return RedirectToAction(nameof(Index));
        }

    }
}
