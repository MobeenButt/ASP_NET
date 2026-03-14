using Lms.Data;
using Lms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    [Authorize]
    public class LessonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LessonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Show lessons of a course
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Index(int courseId)
        {
            var lessons = await _context.Lessons
                .Where(l => l.CourseId == courseId)
                .ToListAsync();

            ViewBag.CourseId = courseId;

            return View(lessons);
        }

        // Admin only
        [Authorize(Roles = "Admin")]
        public IActionResult Create(int courseId)
        {
            ViewBag.CourseId = courseId;
            return View();
        }

        // Save lesson
        [HttpPost]
        [ValidateAntiForgeryToken] // for secure form submission
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                _context.Lessons.Add(lesson);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", new { courseId = lesson.CourseId });
            }
            ViewBag.CourseId = lesson.CourseId; // to keep courseId in case of validation errors
            return View(lesson);
        }
    }
}