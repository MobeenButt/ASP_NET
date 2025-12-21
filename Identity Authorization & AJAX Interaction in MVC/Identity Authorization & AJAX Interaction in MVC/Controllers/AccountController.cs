using Identity_Authorization___AJAX_Interaction_in_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Authorization___AJAX_Interaction_in_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await signInManager.PasswordSignInAsync(username, password, false, false);
            if (!result.Succeeded)
            {
                return View();
            }
            if(User.IsInRole("Admin"))
            {
                return RedirectToAction("Pending", "Admin");
            }
            else
            {
                return RedirectToAction("List", "Resource");
            }
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
