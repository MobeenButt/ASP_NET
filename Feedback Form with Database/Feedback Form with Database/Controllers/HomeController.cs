using Microsoft.AspNetCore.Mvc;

namespace Feedback_Form_with_Database.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "FeedBack Page";
        }
    }
}
