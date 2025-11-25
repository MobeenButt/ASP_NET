namespace MVC_Template.Models
{
    public class StudentRepository
    {
        public static List<Student> students = new List<Student>();
        public static void AddStudent(Student s)
        {
            students.Add(s);
        }

    }
}
