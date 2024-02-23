using Microsoft.AspNetCore.Mvc;

namespace AlcaldiaApp.Controllers
{
    public class ServiceRequests : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
