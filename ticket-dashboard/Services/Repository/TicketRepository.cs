using ticket_dashboard.Data;
using ticket_dashboard.Models;

namespace ticket_dashboard.Services.Repository
{
    public class TicketRepository : Repository<Ticket>
    {
        public TicketRepository(ApplicationDbContext context) : base(context) { }

        public IEnumerable<Ticket> GetByUserId(int userId)
        {
            return _context.Tickets.Where(t => t.UserId == userId).ToList();
        }
    }
}
