namespace WebApplication2.Models
{
    public class StudentRepository
    {
        List<Student> students;
        public StudentRepository()
        {
            students = new List<Student>()
            {
                new Student { Id = 1, Name = "Alice", Age = 20 },
                new Student { Id = 2, Name = "Bob", Age = 22 },
                new Student { Id = 3, Name = "Charlie", Age = 21 }
            };

        }
        public List<Student> GetAllStudents()
        {
            return students;
        }
        public Student GetStudentById(int id)
        {
            return students.Find(s => s.Id == id);
        }
        public void AddStudent(Student st)
        {
            students.Add(st);
        }
        public void DeleteStudent(int id)
        {
            var st = students.Find(s => s.Id == id);
            if (st != null)
            {
                students.Remove(st);
            }
        }
        public void UpdateStudent(Student st,int id)
        {
            var exisitingSt = students.Find(s => s.Id == id);
            if (exisitingSt != null) {
                exisitingSt.Name = st.Name;
                exisitingSt.Age = st.Age;
            }

        }
    }
}
