        using Identity_Authorization___AJAX_Interaction_in_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Authorization___AJAX_Interaction_in_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
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
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
            
            var user = await userManager.FindByNameAsync(username);
            if(user != null && await userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("Pending", "Admin");
            }
            else
            {
                return RedirectToAction("List", "Resource");
            }
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            var user = new ApplicationUser { UserName = username, Email = email };
            var result = await userManager.CreateAsync(user, password);
            
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("List", "Resource");
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
            return RedirectToAction("Login");
        }
        
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
