using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ticket_dashboard.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
