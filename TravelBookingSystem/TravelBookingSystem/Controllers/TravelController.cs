using Microsoft.AspNetCore.Mvc;

namespace TravelBookingSystem.Controllers
{
    public class TravelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
