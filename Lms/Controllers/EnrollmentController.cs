using Lms.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Lms.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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
            var existing = _context.Enrollments.FirstOrDefault(e => e.CourseId == courseId && e.StudentId == user.Id);
            if (existing != null)
            {
                TempData["Message"] = "You have already requested enrollment for this course.";
                return RedirectToAction("Details", "Course", new { id = courseId });

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
            return RedirectToAction("Details", "Course", new { id = courseId });

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            var pending = _context.Enrollments
                .Where(e => e.Status == "Pending")
                .Include(e => e.Course)
                .Include(e => e.Student)
                .ToList();
            return View(pending);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Approve(int id)
        {
            {
                var enrollment = _context.Enrollments.Find(id);
                if (enrollment == null)
                    return NotFound();
                enrollment.Status = "Approved";
                _context.SaveChanges();
                return RedirectToAction(nameof(Dashboard));
            }
        }
    }
}