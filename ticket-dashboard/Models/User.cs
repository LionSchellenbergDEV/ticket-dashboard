using System.Net.Sockets;

namespace ticket_dashboard.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Mitarbeiter";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Property
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
