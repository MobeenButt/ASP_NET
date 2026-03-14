using Lms.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Lms.Data;
using Microsoft.AspNetCore.Authorization;

namespace Lms.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollmentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> RequestEnrollment(int courseId)
        {
            if (User.IsInRole("Admin"))
                return Forbid(); // Admins should not request enrollment

            if (!User.IsInRole("Student"))
                return Forbid(); // Only students can request enrollment

            var user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                return Challenge(); // Redirect to login if user is not authenticated
            }
            var existing=_context.Enrollments.FirstOrDefault(e=>e.CourseId == courseId && e.StudentId == user.Id);
            if(existing!=null)
            {
                TempData["Message"] = "You have already requested enrollment for this course.";
                return RedirectToAction("Index", "Course");
            
            }
            var enrollment = new Enrollment
            {
                CourseId = courseId,
                StudentId = user.Id,
                Status = "Pending"
            };
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Enrollment request sent.";
            return RedirectToAction("Index", "Course");

        }

    }
}
