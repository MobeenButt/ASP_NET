using Identity_Authorization___AJAX_Interaction_in_MVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Authorization___AJAX_Interaction_in_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext dbcontext;
        public AdminController(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public IActionResult Pending()
        {
            var pendingResources = dbcontext.Bookings.Where(r => r.Status == "Pending").ToList();
            return View(pendingResources);
        }
        [HttpPost]
        public IActionResult Approve(int id)
        {
            var booking = dbcontext.Bookings.Find(id);
            booking.Status = "Approved";
            dbcontext.SaveChanges();
            var updated = dbcontext.Bookings.Where(r => r.Status == "Pending").ToList();
            return PartialView("_PendingList", updated);
        }
    }
}
