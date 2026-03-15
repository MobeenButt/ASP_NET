using System.ComponentModel.DataAnnotations;
using System.Globalization;


//  Course Relates with Lessons and Admins (CreatedByAdmin) who create the course, it has a Title and Description. The Id is the primary key for the Course entity. The Lessons property is a collection of Lesson objects that belong to the course, establishing a one-to-many relationship between Course and Lesson.

namespace Lms.Models
{
    public class Course
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        public string? Title { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public string? CreatedByAdmin { get; set; }
        
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
