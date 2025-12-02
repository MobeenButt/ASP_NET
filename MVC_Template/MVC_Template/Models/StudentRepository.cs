namespace MVC_Template.Models
{
    public class StudentRepository
    {
        public static List<Student> students = new List<Student>();
        static StudentRepository()
        {
            students.Add(new Student() { Id = 1, Name = "Alice", Age = 20, CGPA = 3.5f, Semester = "Spring 2023" }); 
            students.Add(new Student() { Id = 2, Name = "Bob", Age = 22, CGPA = 3.8f, Semester = "Fall 2023" });
            students.Add(new Student() { Id = 3, Name = "Charlie", Age = 21, CGPA = 3.2f, Semester = "Spring 2024" });
            students.Add(new Student() {Id = 4, Name = "Diana", Age = 23, CGPA = 3.9f, Semester = "Fall 2024" });
        }
        public static void AddStudent(Student s)
        {
            students.Add(s);
        }

    }
}
