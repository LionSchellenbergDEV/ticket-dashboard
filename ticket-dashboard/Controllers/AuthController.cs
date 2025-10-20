using Microsoft.AspNetCore.Mvc;

namespace ticket_dashboard.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
