using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Travel_Booking_System_with_Identity.Models;
using Travel_Booking_System_with_Identity.Services;

namespace Travel_Booking_System_with_Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITravelService service;
        public HomeController(UserManager<ApplicationUser> userManager,ITravelService service)
        {
            this.userManager = userManager;
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        //USER DASHBAORD

        [Authorize(Roles ="User")]
        public async Task<IActionResult> UserDashboard()
        {
            var user =await userManager.GetUserAsync(User);
            service.SeedSamplePackages();
            var packages = service.GetAllPackages();
            if(user!=null)
            {
                var bookings = service.GetBookingsByUser(user.Email);
                ViewBag.Packages = packages;
                ViewBag.Bookings = bookings;

                return View(bookings);
            }
            return View();
        }
        [Authorize(Roles ="User")]
        [HttpPost]
        public async Task<IActionResult> BookPackage(int packageId, string customerName, string customerEmail, int numberOfTravelers)
        {
            var user = await userManager.GetUserAsync(User);
            var package = service.GetPackageById(packageId);
            if(user!=null && package!=null)
            {
                var booking = new Booking
                {
                    PackageId = packageId,
                    CustomerName = user.UserName,
                    CustomerEmail = user.Email,
                    BookingDate = DateTime.Now,
                    BookingStatus = "Pending"
                };
                service.CreateBooking(booking);
            }
            return RedirectToAction("UserDashboard");
        }

        //ADMIN DASHBOARD

        [Authorize(Roles="Admin")]
        public IActionResult AdminDashboard()
        {
            //service.SeedSamplePackages();
            var bookings = service.GetAllBookings();
            return View(bookings);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult ChangeStatus(int bookingId, string status)
        {
            if(status!="Approved" && status!="Rejected")
            {
                return RedirectToAction("AdminDashboard");
            }

            service.ChangeBookingStatus(bookingId, status);
            return RedirectToAction("AdminDashboard");
        }

    }
}
