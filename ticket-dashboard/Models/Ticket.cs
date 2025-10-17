

namespace ticket_dashboard.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = "Offen";
        public string? Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Beziehungen
        public int? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }

        public ICollection<MailTicket>? MailTickets { get; set; }
    }
}
