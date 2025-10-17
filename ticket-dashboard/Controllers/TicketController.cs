using Microsoft.AspNetCore.Mvc;
using ticket_dashboard.Services;

namespace ticket_dashboard.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            _ticketService.UpdateStatus(id, status);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
