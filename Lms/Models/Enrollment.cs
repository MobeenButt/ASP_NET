namespace Lms.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? StudentId { get; set; }
        public string Status { get; set; } = "Pending";
        public Course Course { get; set; }
    }
}
