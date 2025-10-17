namespace ticket_dashboard.Models.ViewModels
{
    public class DashboardViewModel
    {
        public List<Ticket> UserTickets { get; set; } = new();
        public List<MailTicket> UnassignedMails { get; set; } = new();
        public string Username { get; set; } = string.Empty;
    }
}
