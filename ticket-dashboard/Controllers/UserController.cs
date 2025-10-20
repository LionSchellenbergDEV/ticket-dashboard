using Microsoft.AspNetCore.Mvc;

namespace ticket_dashboard.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
