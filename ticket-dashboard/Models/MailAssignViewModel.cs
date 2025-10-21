namespace ticket_dashboard.Models
{
    public class MailAssignViewModel
    {
        // WICHTIG: Dies ist die IMAP UID oder MessageID, um die Mail später zu löschen.
        public string Uid { get; set; } = string.Empty;

        public string Sender { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
