using Microsoft.AspNetCore.Mvc;

namespace Identity_Authorization___AJAX_Interaction_in_MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
