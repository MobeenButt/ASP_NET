namespace Lms.Models
{

    //   Lesson belongs to Course and has a Title and Content. The Id is the primary key for the Lesson entity. The CourseId is a foreign key that references the Course entity, establishing a many-to-one relationship between Lesson and Course. The Course property is a navigation property that allows access to the related Course object from a Lesson instance.

    public class Lesson
    {
        public int Id { get; set; }
        public string ? Title { get; set; }
            public string ? Content { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
