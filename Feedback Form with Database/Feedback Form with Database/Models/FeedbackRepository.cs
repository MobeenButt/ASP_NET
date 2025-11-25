namespace Feedback_Form_with_Database.Models
{
    public class FeedbackRepository
    {
        public static List<Feedback> feedbacks=new List<Feedback>();

        public static void AddFeedback(Feedback feedback)
        {
            feedbacks.Add(feedback);
        }
        public List<Feedback> GetAllFeedbacks()
        {
            return feedbacks;
        }
    }
}
