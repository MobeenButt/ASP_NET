//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Travel_Booking_System_with_identity_.Data;
//using Travel_Booking_System_with_identity_.Models;
//using Travel_Booking_System_with_identity_.Models.ViewModels;
//namespace Travel_Booking_System_with_identity_.Controllers
//{
//    [Authorize(Roles ="User")]
//    public class DashboardController : Controller
//    {
//        private readonly UserManager<ApplicationUser> userManager;
//        private readonly ApplicationDbContext db;

//        public DashboardController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
//        {
//            this.userManager = userManager;
//            this.db = db;
//        }
//        [HttpGet]
//        public async Task<IActionResult> UserDashboard()
//        {
//            var user = await userManager.GetUserAsync(User);
//            var packages = await db.TravelPackages.ToListAsync();
//            var bookings=await db.Bookings
//                .Include(b=>b.Package)
//                .Where(b=>b.CustomerId==user.Id)
//                .ToListAsync();

//            var usermodel = new UserDashboardViewModel { Packages = packages, Bookings = bookings };

//            return View(usermodel);
//        }

//        [HttpPost]
//        public async Task<IActionResult> BookPackage(BookPackageViewModel model)
//        {
//            if(!ModelState.IsValid)
//            {
//                return View("UserDashboard");
//            }
//            var package=await db.TravelPackages.FindAsync(model.PackageId);
//            if(package==null)
//            {
//                return View("UserDashboard");
//            }
//            var userId =  userManager.GetUserId(User);
//            var booking = new Booking
//            {
//                PackageID = package.PackageID,
//                CustomerId=userId,
//                CustomerName = model.CustomerName,
//                CustomerEmail = model.CustomerEmail,
//                NumberOfTravelers = model.NumberOfTravelers,
//                TotalAmount = package.Price * model.NumberOfTravelers,
//                BookingStatus="Pending"
//            };
//            db.Bookings.Add(booking);
//            await db.SaveChangesAsync();
//            return RedirectToAction("UserDashboard");
//        }

//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Travel_Booking_System_with_identity_.Models;
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
            var bookings = await db.Bookings.Include(b => b.Package).ToListAsync();

            ViewBag.Packages = packages;
            ViewBag.Bookings = bookings;

            return View();
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

            ViewBag.UserBookings = userBookings;
            ViewBag.AvailablePackages = availablePackages;

            return View();
        }
    }
}