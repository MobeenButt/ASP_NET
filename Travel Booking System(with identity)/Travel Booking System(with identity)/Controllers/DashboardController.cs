using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Travel_Booking_System_with_identity_.Models;
using Travel_Booking_System_with_identity_.Models.ViewModels;
using Travel_Booking_System_with_identity_.Data;
using Microsoft.EntityFrameworkCore;

namespace Travel_Booking_System_with_identity_.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.db = context;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDashboard()
        {
            var packages = await db.TravelPackages.ToListAsync();
            var bookings = await db.Bookings
                .Include(b => b.Package)
                .Include(b => b.Customer)
                .ToListAsync();

            return View(bookings);
        }

        [Authorize]
        public async Task<IActionResult> UserDashboard()
        {
            var userId = userManager.GetUserId(User);
            var userBookings = await db.Bookings
                .Include(b => b.Package)
                .Where(b => b.CustomerId == userId)
                .ToListAsync();

            var availablePackages = await db.TravelPackages
                .Where(p => p.AvailableSeats > 0)
                .ToListAsync();

            var model = new UserDashboardViewModel
            {
                Packages = availablePackages,
                Bookings = userBookings
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangeStatus(int bookingId, string status)
        {
            var booking = await db.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                booking.BookingStatus = status;
                await db.SaveChangesAsync();
            }
            return RedirectToAction("AdminDashboard");
        }
    }
}