using Microsoft.AspNetCore.Mvc;

namespace ticket_dashboard.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
