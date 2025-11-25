using Microsoft.AspNetCore.Mvc;
using MVC_Template.Models;
namespace MVC_Template.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ViewResult StudentForm()
        {
            return View();
        }
        //[HttpPost]
        //public ViewResult StudentForm(string name, int age, float cgpa, string semester)
        //{
        //    return View();
        //}
        [HttpPost]
        public ViewResult StudentForm(Student s)
        {
            StudentRepository.AddStudent(s);
            return View("Thanks",s);
        }
        public ViewResult ListStudents()
        {
            return View(StudentRepository.students);
        }

    }
}
