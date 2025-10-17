using ticket_dashboard.Models;
using ticket_dashboard.Services.Repository;

namespace ticket_dashboard.Services
{
    public class TicketService
    {
        private readonly TicketRepository _repository;

        public TicketService(TicketRepository repository)
        {
            _repository = repository;
        }

        public List<Ticket> GetTicketsForUser(int userId)
        {
            return _repository.GetByUserId(userId).ToList();
        }

        public void UpdateStatus(int ticketId, string newStatus)
        {
            var ticket = _repository.GetById(ticketId);
            if (ticket != null)
            {
                ticket.Status = newStatus;
                _repository.Update(ticket);
            }
        }
    }
}
