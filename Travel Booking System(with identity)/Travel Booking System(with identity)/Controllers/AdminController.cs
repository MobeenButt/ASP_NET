using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel_Booking_System_with_identity_.Data;
namespace Travel_Booking_System_with_identity_.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;
        public AdminController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public async Task<IActionResult> AdminDashboard()
        {
            var bookings = await db.Bookings
                .Include(b => b.Package)
                .ToListAsync();
            return View(bookings);
        }
        public async Task<IActionResult> ChangeStatus(int bookingId, string status)
        {
            var booking = await db.Bookings.FindAsync(bookingId);
            if(booking == null)
            {
                return NotFound();
            }
            booking.BookingStatus = status;
            await db.SaveChangesAsync();
            return RedirectToAction("AdminDashboard");
        }
    }
}
