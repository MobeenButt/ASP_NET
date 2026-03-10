namespace Feedback_Form_with_Database.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Course { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }
        public readonly DateTime DateSubmitted = DateTime.Now;

    }
}
