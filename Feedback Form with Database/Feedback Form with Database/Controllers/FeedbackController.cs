using Feedback_Form_with_Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


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

        //public ViewResult GiveFeedback(int id, string name, string course, string comments, int rating, DateTime date)
        //{
        //    return View();
        //}
        [HttpPost]
        public ViewResult GiveFeedback(Feedback feedback)
        {
            FeedbackRepository.AddFeedback(feedback);

            using (SqlConnection con = DbHepler.GetConnectionString())
            {
                con.Open();
                string query = "Insert into Feedbacks(StudentName,Course,Comments,Rating,DateSubmitted) values(@StudentName,@Course,@Comments,@Rating,@DateSubmitted)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StudentName", feedback.StudentName);
                    cmd.Parameters.AddWithValue("@Course", feedback.Course);
                    cmd.Parameters.AddWithValue("@Comments", feedback.Comments);
                    cmd.Parameters.AddWithValue("@Rating", feedback.Rating);
                    cmd.Parameters.AddWithValue("@DateSubmitted", feedback.DateSubmitted);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            //return View();
            return View("FeedbackSummary", feedback);
            //return RedirectToAction("FeedbackSummary", feedback);
        }

        [HttpGet]
        public ViewResult FeedbackSummary(Feedback feedback)
        {
            return View("FeedbackSummary", feedback);
        }
        [HttpGet]
        public ViewResult AllFeedbacks()
        {
            List<Feedback> list = new List<Feedback>();
            using (SqlConnection con = DbHepler.GetConnectionString())
            {
                con.Open();
                string query = "Select * from Feedbacks";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Feedback f = new Feedback();
                    f.Id = dr.GetInt32(0);
                    f.StudentName = dr.GetString(1);
                    f.Course = dr.GetString(2);
                    f.Comments = dr.GetString(3);
                    f.Rating = dr.GetInt32(4);
                    //f.DateSubmitted = dr.GetDateTime(5);  as a READONLY FIELD Submitted via constructor only
                    list.Add(f);
                }
                con.Close();
            }
            return View(list);
        }
    }
}
