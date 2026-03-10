using Identity_Authorization___AJAX_Interaction_in_MVC.Data;
using Identity_Authorization___AJAX_Interaction_in_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Authorization___AJAX_Interaction_in_MVC.Controllers
{
    [Authorize(Roles = "User")]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly UserManager<ApplicationUser> userManager;
        public BookingController(ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager)
        {
            this.dbcontext = dbcontext;
            this.userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Added CSRF validation
        public async Task<IActionResult> Request(int resourceId)
        {
            var user = await userManager.GetUserAsync(User);
            bool exists = dbcontext.Bookings.Any(b => b.ResourceId == resourceId && b.UserId == user.Id);
            if (exists)
            {
                return BadRequest("You have already requested this resource.");
            }

            var resource = dbcontext.Resources.Find(resourceId);
            if (resource == null)
            {
                return NotFound("Resource not found.");
            }

            var booking = new Booking
            {
                ResourceId = resourceId,
                UserId = user.Id,
                BookingDate = DateTime.Now,
                ResourceName = resource.Name,
                ResourceType = resource.Type,
                ResourceLocation = resource.Location,
                Status = "Pending"
            };
            dbcontext.Bookings.Add(booking);
            await dbcontext.SaveChangesAsync();
            return Ok("Booking requested successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(int resourceId)
        {
            var user = await userManager.GetUserAsync(User);
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