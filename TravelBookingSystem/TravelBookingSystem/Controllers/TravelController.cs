using Microsoft.AspNetCore.Mvc;

namespace TravelBookingSystem.Controllers
{
    public class TravelController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {

        }
        [HttpGet]
        public IActionResult Book(int id) { 
        }
        [HttpPost]
        public IActionResult Book()
        {

        }
    }
}
