using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //REST ful Services
        StudentRepository repo;

        public StudentController()
        {
            repo = new StudentRepository();
        }
        [HttpGet]
        [Route("get")]
        public List<Student> Get()
        {
            return repo.GetAllStudents();
        }
        [HttpGet]
        //[HttpGet("{id}")]
        [Route("find/{id}")]
        public Student GetById(int id)
        {
            return repo.GetStudentById(id);
        }
        [HttpPost]
        [Route("add")]
        public void Add(Student st)
        {
            repo.AddStudent(st);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public void Delete(int id)
        {
            repo.DeleteStudent(id);
        }
        [HttpPut]
        [Route("update/{id}")]
        public void Update(Student st,int id)
        {
            repo.UpdateStudent(st,id);
        }
    }
}
