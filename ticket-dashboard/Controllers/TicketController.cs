using Microsoft.AspNetCore.Mvc;

namespace ticket_dashboard.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
