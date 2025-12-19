using Identity_Authorization___AJAX_Interaction_in_MVC.Data;
using Identity_Authorization___AJAX_Interaction_in_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Authorization___AJAX_Interaction_in_MVC.Controllers
{
    [Authorize(Roles ="User")]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly UserManager<ApplicationUser> userManager;
        public BookingController(ApplicationDbContext dbcontext,UserManager<ApplicationUser> userManager)
        {
            this.dbcontext = dbcontext;
            this.userManager = userManager;
        }

    }
}
