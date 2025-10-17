using Microsoft.AspNetCore.Mvc;

namespace ticket_dashboard.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
