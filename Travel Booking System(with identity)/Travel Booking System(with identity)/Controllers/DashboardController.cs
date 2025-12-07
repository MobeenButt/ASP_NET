using Microsoft.AspNetCore.Mvc;

namespace Travel_Booking_System_with_identity_.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
