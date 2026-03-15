using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Lms.Models;
using Lms.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lms.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // View all courses - Both Admin and Student can view
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }

        // Admin can access Create Course Page - ONLY ADMIN
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.AsNoTracking().ToList(), "Id", "Name");
            return View();
        }

        // Save Course - ONLY ADMIN
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Get the currently logged-in user and set the CreatedByAdmin property
                    var user = await _userManager.GetUserAsync(User);
                    course.CreatedByAdmin = user.Id;
                    _context.Courses.Add(course);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                // Log validation errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    System.Diagnostics.Debug.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving the course.");
            }

            ViewBag.Categories = new SelectList(_context.Categories.AsNoTracking().ToList(), "Id", "Name", course.CategoryId);
            return View(course);
        }


        //load course details - Both Admin and Student can view
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Lessons)
                .Include(c => c.Category) // Include category details
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
    }
}
