using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Lms.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        //one to many relationship with Course
        // means one category can have many courses but each course belongs to only one category
        public List<Course> Courses { get; set; } = new();
    }
}
