using Feedback_Form_with_Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Feedback_Form_with_Database.Controllers
{
    public class FeedbackController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }
        [HttpGet]
        public ViewResult GiveFeedback()
        {
            return View();
        }
        [HttpPost]
        //public ViewResult GiveFeedback(int id, string name, string course, string comments, int rating, DateTime date)
        //{
        //    return View();
        //}
        public ViewResult GiveFeedback(Feedback feedback)
        {
            return View();
        }

        [HttpGet]
        public ViewResult FeedbackSummary(Feedback feedback)
        {
            return View();
        }
        [HttpGet]
        public ViewResult AllFeedbacks()
        {
            return View();
        }
    }
}
