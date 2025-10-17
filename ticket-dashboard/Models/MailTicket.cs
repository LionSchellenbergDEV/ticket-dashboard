namespace ticket_dashboard.Models
{
    public class MailTicket
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
        public DateTime ReceivedAt { get; set; } = DateTime.Now;
        public bool IsAssigned { get; set; } = false;
    }
}
