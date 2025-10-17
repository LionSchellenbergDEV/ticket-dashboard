using Microsoft.AspNetCore.Mvc;
using ticket_dashboard.Data;

namespace ticket_dashboard.Controllers
{
    public class DatabaseTestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DatabaseTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return Content($"Verbundene Benutzer: {users.Count}");
        }
    }
}
