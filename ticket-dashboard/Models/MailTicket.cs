namespace ticket_dashboard.Models
{
    public class MailTicket
    {
        public int MailTicketId { get; set; }
        public string MailId { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string? Body { get; set; }
        public DateTime ReceivedAt { get; set; } = DateTime.Now;

        // Beziehung zu Ticket
        public int? TicketId { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
