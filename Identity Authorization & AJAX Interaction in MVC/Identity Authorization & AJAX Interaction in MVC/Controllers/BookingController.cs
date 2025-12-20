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
        [HttpPost]
        public async Task<IActionResult> CreateBooking(int resourceId)
        {
            var user =await userManager.GetUserAsync(User);
            bool exists = dbcontext.Bookings.Any(b => b.ResourceId == resourceId && b.UserId == user.Id);
            if (exists)
            {
                ModelState.AddModelError("", "You have already booked this resource.");
                return View();
            }

            var booking = new Booking
            {
                ResourceId = resourceId,
                UserId = user.Id,
                BookingDate = DateTime.Now
            };
            dbcontext.Bookings.Add(booking);
            await dbcontext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
