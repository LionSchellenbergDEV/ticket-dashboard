using Microsoft.AspNetCore.Mvc;
using ticket_dashboard.Models.ViewModels;
using ticket_dashboard.Services;

namespace ticket_dashboard.Controllers
{
    public class DashboardController : Controller
    {
        private readonly TicketService _ticketService;
        private readonly MailService _mailService;

        public DashboardController(TicketService ticketService, MailService mailService)
        {
            _ticketService = ticketService;
            _mailService = mailService;
        }

        public IActionResult Index(int userId)
        {
            var vm = new DashboardViewModel
            {
                UserTickets = _ticketService.GetTicketsForUser(userId),
                UnassignedMails = _mailService.GetUnassignedMails(),
                Username = "DemoUser"
            };
            return View(vm);
        }
    }
}
