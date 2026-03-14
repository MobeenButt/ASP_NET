using Lms.Models;
using Lms.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// DbContext class that inherits from IdentityDbContext to include Identity features for user management. It defines DbSet properties for Courses, Lessons, and Enrollments, allowing Entity Framework Core to manage these entities in the database. The constructor takes DbContextOptions and passes them to the base class constructor, enabling configuration of the database connection and other options when setting up the application. Purpose: Bridge between C# objects and database tables, enabling CRUD operations and managing relationships between entities in the context of an LMS application.

namespace Lms.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }

}
