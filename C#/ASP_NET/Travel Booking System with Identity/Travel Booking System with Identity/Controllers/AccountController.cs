using Microsoft.AspNetCore.Mvc;
using Travel_Booking_System_with_Identity.Models;
using Microsoft.AspNetCore.Identity;
namespace Travel_Booking_System_with_Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await signInManager.PasswordSignInAsync(email, password, false, false);
           if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }
           var user = await userManager.FindByEmailAsync(email);
            if(user!=null && await userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("AdminDashboard", "Home");
            }
            return RedirectToAction("UserDashboard", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            var user = new ApplicationUser  { UserName = email, Email = email };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");//default role
                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("UserDashboard", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login" +
                "");
        }
    }
}
