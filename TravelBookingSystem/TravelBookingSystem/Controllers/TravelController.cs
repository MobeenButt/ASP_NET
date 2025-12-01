using Microsoft.AspNetCore.Mvc;
using TravelBookingSystem.Models;
using TravelBookingSystem.Services;

namespace TravelBookingSystem.Controllers
{
    public class TravelController : Controller
    {
        public readonly ITravelService service;
        public TravelController (ITravelService service)
        {
            this.service = service;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var packages = service.GetAllPackages();
            if(packages == null || packages.Count == 0)
            {
                ViewBag.Message = "No travel packages available at the moment.";
            }
            return View(packages);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var packageById = service.GetPackageById(id);
            if (packageById == null)
            {
                return NotFound();
            }
            ViewBag.Package = packageById;
            return View();
        }
        [HttpGet]
        public IActionResult Book(int id)
        {
            var booking = service.GetPackageById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }
        [HttpPost]
        public IActionResult Book(Booking model)
        {
            //total amount
            var pkgInfo=service.GetPackageById(model.PackageID);
            model.TotalAmount = pkgInfo.Price * model.NumberOfTravelers;
            bool success = service.CreateBooking(model);
            if(success)
            {
                ViewBag.Message= "Booking Successful!";
                ViewBag.Booking=model;
                return View("Success");
            }
            ViewBag.Message= "Booking Failed. Please try again.";
            ViewBag.Booking = model;
            return View("Failure");
        }
    }
}
