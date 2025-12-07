using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Travel_Booking_System_with_identity_.Models.ViewModels;
using Travel_Booking_System_with_identity_.Models;

namespace Travel_Booking_System_with_identity_.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            this.userManager = userManager;
            this.signManager = signManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
                return View(model);
            }
            // assign User role by default
            await userManager.AddToRoleAsync(user, "User");
            await signManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("UserDashboard", "Dashboard");

        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl=null)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await signManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Login");
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("AdminDashboard", "Dashboard");
            }
            else
            {
                return RedirectToAction("UserDashboard", "Dashboard");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
