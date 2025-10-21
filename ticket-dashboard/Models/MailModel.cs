using MailKit;

namespace ticket_dashboard.Models
{
    public class MailModel
    {
        public UniqueId Uid { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
