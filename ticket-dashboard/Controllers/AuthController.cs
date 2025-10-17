using Microsoft.AspNetCore.Mvc;
using ticket_dashboard.Services;

namespace ticket_dashboard.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _authService.ValidateUser(username, password);
            if (user != null)
            {
                return RedirectToAction("Index", "Dashboard", new { userId = user.Id });
            }
            ViewBag.Error = "Ungültige Anmeldedaten";
            return View();
        }
    }
}
