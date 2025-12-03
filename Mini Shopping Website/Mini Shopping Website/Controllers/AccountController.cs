using Microsoft.AspNetCore.Mvc;
using Mini_Shopping_Website.Models;
using Microsoft.AspNetCore.Session;
using System;
using System.Web;

namespace Mini_Shopping_Website.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Users model)
        {
            if (model.Email == "mobeen914butt@gmail.com" && model.Password == "12345")
            {
                HttpContext.Session.SetString("Email", model.Email);
                HttpContext.Session.SetString("Password", model.Password);
                return View("Home");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
        [HttpGet]
        public IActionResult Home()
        {
            var email = HttpContext.Session.GetString("Email");
            if(email==null)
            {
                return View("Login");
            }
            return View();
        }
    }
}
