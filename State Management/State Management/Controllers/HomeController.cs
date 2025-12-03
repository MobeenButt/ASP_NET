using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using State_Management.Models;

namespace State_Management.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string data = String.Empty;
            if (HttpContext.Request.Cookies.ContainsKey("first_request"))
            {
                string firstVisitedDaateTime = HttpContext.Request.Cookies["first_request"];
                data = "Welcome Back"+ firstVisitedDaateTime;

            }
            else
            {
                //    var cookieOptions = new CookieOptions
                //    {
                //        Expires = DateTime.Now.AddDays(30), // Cookie expires in 30 days
                //        HttpOnly = true,
                //        Secure = true,
                //        SameSite = SameSiteMode.Strict
                //    };
                CookieOptions option = new CookieOptions();
                option.Expires = System.DateTime.Now.AddDays(1);
                data = "you visited first time";

                HttpContext.Response.Cookies.Append("first_request", System.DateTime.Now.ToString(),option);
            }
            return View("index",data);
        }

        public IActionResult Remove()
        {
            HttpContext.Response.Cookies.Delete("first_request");
            return View("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
