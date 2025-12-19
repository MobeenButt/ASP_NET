using Identity_Authorization___AJAX_Interaction_in_MVC.Data;
using Identity_Authorization___AJAX_Interaction_in_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Identity_Authorization___AJAX_Interaction_in_MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ResourceController : Controller
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly UserManager<ApplicationUser> userManager;
        public ResourceController(ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager)
        {
            this.dbcontext = dbcontext;
            this.userManager = userManager;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Resource resource)
        {
            if (!ModelState.IsValid)
            {
                return View(resource);
            }
            var user = await userManager.GetUserAsync(User);
            resource.CreatedByUserId = user.Id;

            dbcontext.Resources.Add(resource);
            await dbcontext.SaveChangesAsync();

            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View(dbcontext.Resources.ToList());
        }
        
    }
}
