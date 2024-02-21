using AlcaldiaApp.Models;
using AlcaldiaApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AlcaldiaApp.Controllers
{
    public class PositionController : Controller
    {
        private readonly IPositionRepository _positionRepository;

        public PositionController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }


        public IActionResult Index()
        {
            return View(_positionRepository.GetAllPositions());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PositionModel positionModel)
        {
            if (!ModelState.IsValid)
            {
                return View(positionModel);
            }

            _positionRepository.Add(positionModel);
            return RedirectToAction(nameof(Index));
        }

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
            if (!ModelState.IsValid)
            {
                return View(positionModel);
            }

            _positionRepository.Edit(positionModel);
            return RedirectToAction(nameof(Index));
        }

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

            return RedirectToAction(nameof(Index));
        }
    }
}
